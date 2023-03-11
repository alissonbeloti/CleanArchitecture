using AutoMapper;

using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Streamers.Queries.Vms;
using CleanArchitecture.Domain;

using MediatR;

using System.Linq.Expressions;

namespace CleanArchitecture.Application.Features.Streamers.Queries.GetStreamerListByUsername;

public class GetStreamerListQueryHandler : IRequestHandler<GetStreamerListQuery, List<StreamerVm>>
{
    private readonly IUnityOfWork _unityOfWork;
    private readonly IMapper _mapper;

    public GetStreamerListQueryHandler(IUnityOfWork unityOfWork, IMapper mapper)
    {
        _unityOfWork = unityOfWork;
        _mapper = mapper;
    }

    public async Task<List<StreamerVm>> Handle(GetStreamerListQuery request, CancellationToken cancellationToken)
    {
        var includes = new List<Expression<Func<Streamer, object>>>();
        includes.Add(p => p.Videos!);

        var streamerList = await _unityOfWork.Repository<Streamer>().GetAsync(
            x => x.CreatedBy == request.Username,
            o => o.OrderBy(x => x.CreatedDate),
            includes,
            true
            );

        return _mapper.Map<List<StreamerVm>>(streamerList);

    }
}
