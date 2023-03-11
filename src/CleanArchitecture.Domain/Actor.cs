using CleanArchitecture.Domain.Common;

using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Domain;

public class Actor: BaseDomainModel
{
    public Actor()
    {
        Videos = new HashSet<Video>();
    }
    public string? Nome { get; set; }
    public string? Sobrenome { get; set; }

    [NotMapped]
    public string NomeCompleto => $"{Nome} {Sobrenome}";
    public virtual ICollection<Video> Videos { get; set; }
}
