using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Specifications.Videos;

public class VideoSpecification: BaseSpecification<Video>
{
    public VideoSpecification(VideoSpecificationParams videoParams)
    : base(
        x => (string.IsNullOrEmpty(videoParams.Search) || x.Nome!.Contains(videoParams.Search)) &&
            (!videoParams.DirectorId.HasValue || x.DirectorId == videoParams.DirectorId.Value) &&
            (!videoParams.StreamerId.HasValue || x.StreamerId == videoParams.StreamerId.Value)
        )
    {
        AddInclude(p => p.Director!);
        AddInclude(p => p.Streamer!);
        AddInclude(p => p.Atores!);

        ApplyPaging(videoParams.PageSize * (videoParams.PageIndex - 1), videoParams.PageSize);

        if (!string.IsNullOrEmpty(videoParams.Sort))
        {
            switch (videoParams.Sort) 
            {
                case "nomeAsc":
                    AddOrderBy(p => p.Nome!);
                    break;
                case "nomeDesc":
                    AddOrderByDescending(p => p.Nome!);
                    break;
                default:
                    AddOrderBy(p => p.CreatedDate!);
                    break;
            }
        }
    }
}
