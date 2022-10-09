using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                UserId = "6551e1b0-0d93-47c1-8169-7cd58f92e4a7",
                RoleId = "efbb131d-d817-4bb3-b9f8-7327f9416c09"
            },
            new IdentityUserRole<string>
            {
                UserId = "373b81c1-ba8c-4ed3-91da-ade3cd4ee49c",
                RoleId = "17841ac7-fe7f-418d-88c6-7bdb42cc8cf3"
            }
        );
    }
}
