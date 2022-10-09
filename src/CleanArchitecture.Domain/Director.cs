using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain;

public class Director: BaseDomainModel
{
    public string? Nome { get; set; }
    public string? Sobrenome { get; set; }
    public int VideoId { get; set; }
    public virtual Video? Video { get; set; }

}
