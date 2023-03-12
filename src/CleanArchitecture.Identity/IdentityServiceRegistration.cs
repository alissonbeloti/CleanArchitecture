using System.Text;

using CleanArchitecture.Identity.Models;
using CleanArchitecture.Identity.Services;
using CleanArchitecture.Application.Models.Identity;
using CleanArchitecture.Application.Contracts.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace CleanArchitecture.Identity;

public static class IdentityServiceRegistration
{
    public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddDbContext<CleanArchitectureIdentityDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("IdentityConnectionString"),
            b => b.MigrationsAssembly(typeof(CleanArchitectureIdentityDbContext).Assembly.FullName)));

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<CleanArchitectureIdentityDbContext>();

        services.AddTransient<IAuthService, AuthService>();
        var tokenValidationParameter = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            RequireExpirationTime = false,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtSettings:Key"]))
        };

        services.AddSingleton(tokenValidationParameter);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = tokenValidationParameter;
        });


        return services;
    }
}
