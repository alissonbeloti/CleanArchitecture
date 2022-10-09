using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain;

public class Actor: BaseDomainModel
{
    public Actor()
    {
        Videos = new HashSet<Video>();
    }
    public string? Nome { get; set; }
    public string? Sobrenome { get; set; }

    public virtual ICollection<Video> Videos { get; set; }
}
