using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain;

public class Video: BaseDomainModel
{
    public Video()
    {
        Atores = new HashSet<Actor>();
    }
    public string? Nome { get; set; }

    public int StreamerId { get; set; }
    public virtual Streamer? Streamer { get; set; }

    public int DirectorId { get; set; }
    public virtual Director? Director { get; set; }
    public virtual ICollection<Actor>? Atores { get; set; }

}
