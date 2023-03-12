using CleanArchitecture.Application.Contants;
using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Identity;
using CleanArchitecture.Identity.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System.Formats.Asn1;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Identity.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly JwtSettings _jwtSettings;
    private readonly CleanArchitectureIdentityDbContext _cleanArchitectureDbContext;
    private readonly TokenValidationParameters _tokenValidationParameters;
    public AuthService(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IOptions<JwtSettings> jwtSettings,
        CleanArchitectureIdentityDbContext cleanArchitectureDbContext,
        TokenValidationParameters tokenValidationParameters)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSettings = jwtSettings.Value;
        _cleanArchitectureDbContext = cleanArchitectureDbContext;
        _tokenValidationParameters = tokenValidationParameters;
    }

    public async Task<AuthResponse> Login(AuthRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            throw new Exception($"O usuário com email: {request.Email} não existe");
        }

        var resultado = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

        if (!resultado.Succeeded)
        {
            throw new Exception("Credenciais incorretas.");
        }

        var token = await GenereateToken(user);

        var authResponse = new AuthResponse
        {
            Id = user.Id,
            Email = user.Email,
            Token = token.Item1,
            UserName = user.UserName,
            RefreshToken = token.Item2,
            Success = true,
        };

        return authResponse;

    }

    public Task<AuthResponse> Logout(AuthRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<AuthResponse> RefreshToken(TokenRequest request)
    {
        JwtSecurityTokenHandler jwtTokenHandler = new();
        var tokenValidationParamsClone = _tokenValidationParameters.Clone();
        tokenValidationParamsClone.ValidateLifetime = false;

        try 
        {
            //validation em formato de token é correto
            var tokenVerification = jwtTokenHandler.ValidateToken(
                request.Token, 
                tokenValidationParamsClone, 
                out var validateToken);

            //Validar algoritmo de encriptação
            if (validateToken is JwtSecurityToken jwtSecurityToken) 
            {
                var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature,
                    StringComparison.InvariantCultureIgnoreCase);
                if (!result) 
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Errors = new List<string>
                        {
                            "O Token tem erros de encriptação.",
                        }
                    };
                }
            }

            //Validação: Verificar data de expiração.
            var utcExpiryDate = long.Parse(tokenVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp)!.Value);

            var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

            if (expiryDate > DateTime.UtcNow)
            {
                return new AuthResponse
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "Token está expirado"
                    }
                };
            }

            // validação: refresh existe na base de dados?
            var storedToken = await _cleanArchitectureDbContext.RefreshTokens!.FirstOrDefaultAsync(x => x.Token == request.RefreshToken);
            if (storedToken is null)
            {
                return new AuthResponse
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "Token não existe"
                    }
                };
            }
            //token já foi usado?
            if (storedToken.IsUsed)
            {
                return new AuthResponse
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "Token já foi usado"
                    }
                };
            }

            //Token foi revogado?
            if (storedToken.IsRevoked)
            {
                return new AuthResponse
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "Token revogado!"
                    }
                };
            }

            //Validar id do token
            var jti = tokenVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)!.Value;

            if (storedToken.JwtId != jti)
            {
                return new AuthResponse
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "Token não concorda com valor inicial"
                    }
                };
            }

            // segunda validação para data de expiração
            if (storedToken.ExpireDate <  DateTime.UtcNow)
            {
                return new AuthResponse
                {
                    Success = false,
                    Errors = new List<string>
                    {
                        "expirado"
                    }
                };
            }

            storedToken.IsUsed = true;

            _cleanArchitectureDbContext.RefreshTokens!.Update(storedToken);
            await _cleanArchitectureDbContext.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(storedToken.UserId);
            var token = await GenereateToken(user);

            return new AuthResponse
            {
                Success = true,
                Id = user.Id,
                Token = token.Item1,
                Email = user.Email,
                UserName = user.UserName,
                RefreshToken = token.Item2,
            };
        }
        catch (Exception ex) 
        {
            if (ex.Message.Contains("Lifetime  validation failed. The token is expired"))
            {
                return new AuthResponse
                {
                    Success = false,
                    Errors = new List<string> { "O token está expirado, por favor, realizar login novamente." }
                };
            }
            else
            {
                return new AuthResponse
                {
                    Success = false,
                    Errors = new List<string> { "O token possui erros, por favor, realizar login novamente." }
                };
            }
        }


    }

    private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
    {
        var dateTimeval = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        dateTimeval = dateTimeval.AddSeconds(unixTimeStamp).ToUniversalTime();
        return dateTimeval;
    }

    public async Task<RegistrationResponse> Register(RegistrationRequest request)
    {
        var existingUser = await _userManager.FindByNameAsync(request.Username);
        if (existingUser != null)
            throw new Exception($"O usuário {request.Username} já existe");

        var existingEmail = await _userManager.FindByNameAsync(request.Email);

        if (existingEmail != null)
            throw new Exception($"O e-mail {request.Email} já existe");

        var user = new IdentityUser
        {
            Email = request.Email,
            UserName = request.Username,
            EmailConfirmed = true,
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            var applicationUser = new ApplicationUser
            {
                IdentityId = new Guid(user.Id),
                Email = request.Email,
                Country = request.Country,
                Nome = request.Nome,
                Sobrenome = request.Sobrenome,
                Phone = request.Phone,
            };

            _cleanArchitectureDbContext.ApplicationUsers!.Add(applicationUser);
            await _cleanArchitectureDbContext.SaveChangesAsync();

            var token = await GenereateToken(user);

            return new RegistrationResponse
            {
                Email = user.Email,
                Token = token.Item1,
                UserId = user.Id,
                Username = user.UserName,
                RefreshToken = token.Item2,
            };
        }

        throw new Exception($"Erro: {result.Errors}");
    }
    /*(JwtSecurityToken, RefreshToken)*/
    private async Task<Tuple<string, string>> GenereateToken(IdentityUser user)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Key));

        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        List<Claim> rolesClaims = new();

        foreach (var role in roles)
        {
            rolesClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokenDescritptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            }.Union(userClaims).Union(rolesClaims)),
            Expires = DateTime.UtcNow.Add(_jwtSettings.ExpireTime),
            SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature),

        };

        var token = jwtTokenHandler.CreateToken(tokenDescritptor);
        var jwtToken = jwtTokenHandler.WriteToken(token);

        var refreshToken = new RefreshToken
        {
            JwtId = token.Id,
            IsUsed = false,
            IsRevoked = false,
            UserId = user.Id,
            CreatedDate = DateTime.UtcNow,
            ExpireDate = DateTime.UtcNow.AddMonths(6),
            Token = $"{GenereateRandomTokenCharacters(35)}{Guid.NewGuid()}"
        };

        await _cleanArchitectureDbContext.RefreshTokens!.AddAsync(refreshToken);
        await _cleanArchitectureDbContext.SaveChangesAsync();

        return new Tuple<string, string>(jwtToken, refreshToken.Token);
    }
    private string GenereateRandomTokenCharacters(int length)
    {
        Random random = new();
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        return new string(Enumerable.Repeat(chars, length).Select(x => x[random.Next(x.Length)]).ToArray());
    }
}
