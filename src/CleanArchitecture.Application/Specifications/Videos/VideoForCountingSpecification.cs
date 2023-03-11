using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Specifications.Videos;

public class VideoForCountingSpecification : BaseSpecification<Video>
{
    public VideoForCountingSpecification(VideoSpecificationParams videoParams)
        : base(
            x => (string.IsNullOrEmpty(videoParams.Search) || x.Nome!.Contains(videoParams.Search)) &&
                (!videoParams.DirectorId.HasValue || x.DirectorId == videoParams.DirectorId.Value) &&
                (!videoParams.StreamerId.HasValue || x.StreamerId == videoParams.StreamerId.Value)
            )
    {
    }
}
