using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;

public class GetVideosListQuery : IRequest<List<VideosVm>>
{
    public string _userName { get; set; } = String.Empty;

    public GetVideosListQuery(string? userName)
    {
        _userName = userName ?? throw new ArgumentException(nameof(userName));
    }
}
