using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain;

public class Streamer: BaseDomainModel
{
    public string? Nome { get; set; }

    public string? Url { get; set; }

    public ICollection<Video>? Videos { get; set; }

}
