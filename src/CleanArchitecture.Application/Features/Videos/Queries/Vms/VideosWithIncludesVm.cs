using CleanArchitecture.Application.Features.Actors.Queries.Vms;

namespace CleanArchitecture.Application.Features.Videos.Queries.Vms
{
    public class VideosWithIncludesVm
    {
        public string? Nome { get; set; }
        public int StreamerId { get; set; }
        public string? StreamerNome { get; set; }
        public int DirectorId { get; set; }
        public  string? DirectorNomeCompleto { get; set; }
        public virtual ICollection<ActorVm>? Actors { get; set; }
    }
}
