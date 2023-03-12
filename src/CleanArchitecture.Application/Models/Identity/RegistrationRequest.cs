namespace CleanArchitecture.Application.Models.Identity;

public class RegistrationRequest
{
    public string Nome { get; set; } = string.Empty;
    public string Sobrenome { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}
