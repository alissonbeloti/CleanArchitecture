using MediatR;
using CleanArchitecture.Application.Features.Streamers.Queries.Vms;

namespace CleanArchitecture.Application.Features.Streamers.Queries.GetStreamerListByUrlQuery;

public class GetStreamerListByUrlQuery: IRequest<List<StreamerVm>>
{
    public string? Url { get; set; }

    public GetStreamerListByUrlQuery(string url)
    {
        Url = url ?? throw new ArgumentNullException(nameof(url));
    }
}
