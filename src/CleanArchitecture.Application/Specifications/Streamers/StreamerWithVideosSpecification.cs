using CleanArchitecture.Domain;


namespace CleanArchitecture.Application.Specifications.Streamers;

public class StreamerWithVideosSpecification : BaseSpecification<Streamer>
{
    public StreamerWithVideosSpecification()
    {
        AddInclude(p => p.Videos!);
    }

    public StreamerWithVideosSpecification(string url): base (p => p.Url!.Contains(url))
    {
        AddInclude(p => p.Videos!);
    }

}
