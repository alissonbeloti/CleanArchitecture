using CleanArchitecture.Application.Contants;
using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Identity;
using CleanArchitecture.Identity.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System.Formats.Asn1;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Identity.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtSettings _jwtSettings;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSettings = jwtSettings.Value;
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
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            UserName = user.UserName,
        };

        return authResponse;

    }

    public Task<AuthResponse> Logout(AuthRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<RegistrationResponse> Register(RegistrationRequest request)
    {
        var existingUser = await _userManager.FindByNameAsync(request.Username);
        if (existingUser != null)
            throw new Exception($"O usuário {request.Username} já existe");

        var existingEmail = await _userManager.FindByNameAsync(request.Email);

        if (existingEmail != null)
            throw new Exception($"O e-mail {request.Email} já existe");

        var user = new ApplicationUser
        {
            Email = request.Email,
            Sobrenome = request.Sobrenome,
            Nome = request.Nome,
            UserName = request.Username,
            EmailConfirmed = true,
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Operator");

            var token = await GenereateToken(user);

            return new RegistrationResponse
            {
                Email = user.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserId = user.Id,
                Username = user.UserName,
            };
        }

        throw new Exception($"Erro: {result.Errors}");
    }

    private async Task<JwtSecurityToken> GenereateToken(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        List<Claim> rolesClaims = new();

        foreach (var role in roles)
        {
            rolesClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(CustomClaimTypes.Uid, user.Id)
        }
        .Union(userClaims)
        .Union(rolesClaims);

        SymmetricSecurityKey sysmmetricSecurityKey = new(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        SigningCredentials signingCredentials = new(sysmmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtSecurityToken = new(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes((double)_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials
            );

        return jwtSecurityToken;
    }
}
