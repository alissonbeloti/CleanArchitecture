using MediatR;
using AutoMapper;
using CleanArchitecture.Domain;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Specifications.Streamers;
using CleanArchitecture.Application.Features.Streamers.Queries.Vms;

namespace CleanArchitecture.Application.Features.Streamers.Queries.GetStreamerListByUrlQuery;

public class GetStreamerListByUrlQueryHandler : IRequestHandler<GetStreamerListByUrlQuery, List<StreamerVm>>
{
    private readonly IUnityOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetStreamerListByUrlQueryHandler(IUnityOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<StreamerVm>> Handle(GetStreamerListByUrlQuery request, CancellationToken cancellationToken)
    {
        var spec = new StreamerWithVideosSpecification(request.Url!);
        var streamerList = await _unitOfWork.Repository<Streamer>().GetAllWithSpec(spec);

        return _mapper.Map<List<StreamerVm>>(streamerList);
    }
}
