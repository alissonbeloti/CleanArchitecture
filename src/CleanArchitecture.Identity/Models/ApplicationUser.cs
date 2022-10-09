using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Identity.Models;

public class ApplicationUser: IdentityUser
{
    public string Nome { get; set; } = string.Empty;
    public string Sobrenome { get; set; } = string.Empty;
}
