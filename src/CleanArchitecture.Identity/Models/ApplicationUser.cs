namespace CleanArchitecture.Identity.Models;

public class ApplicationUser: BaseDomainModel
{
    public Guid IdentityId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Sobrenome { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public DateTime DateOfBird { get; set; }

    public string Country { get; set; } = string.Empty;

}
