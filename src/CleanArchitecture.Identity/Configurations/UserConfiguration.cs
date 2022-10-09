using CleanArchitecture.Identity.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();

        builder.HasData(new ApplicationUser { Id = "6551e1b0-0d93-47c1-8169-7cd58f92e4a7",
            Email = "adim@localhost.com",
            NormalizedEmail = "adim@localhost.com",
            Nome = "Alisson",
            Sobrenome = "Beloti",
            UserName = "alissonbeloti",
            NormalizedUserName = "alissonbeloti",
            PasswordHash = hasher.HashPassword(null, "AlissonBeloti2025$"),
            EmailConfirmed = true,

        },
        new ApplicationUser
        {
            Id = "373b81c1-ba8c-4ed3-91da-ade3cd4ee49c",
            Email = "joaomaria@localhost.com",
            NormalizedEmail = "adim@localhost.com",
            Nome = "João",
            Sobrenome = "Maria",
            UserName = "joaomaria",
            NormalizedUserName = "joaomaria",
            PasswordHash = hasher.HashPassword(null, "AlissonBeloti2025$"),
            EmailConfirmed = true,
        }
        );
    }
}
