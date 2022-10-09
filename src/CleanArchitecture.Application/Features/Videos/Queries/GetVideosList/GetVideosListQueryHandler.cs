using AutoMapper;

using CleanArchitecture.Application.Contracts.Persistence;

using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;

public class GetVideosListQueryHandler : IRequestHandler<GetVideosListQuery, List<VideosVm>>
{
    //private readonly IVideoRepository _videoRepository;
    private readonly IUnityOfWork _unityOfWork;
    private readonly IMapper _mapper;
    public GetVideosListQueryHandler(IUnityOfWork unityOfWork, IMapper mapper)
    {
        _unityOfWork = unityOfWork;
        //_videoRepository = videoRepository;
        _mapper = mapper;
    }

    public async Task<List<VideosVm>> Handle(GetVideosListQuery request, CancellationToken cancellationToken)
    {
        var videoList = await _unityOfWork.VideoRepository.GetByUserName(request._userName);

        return _mapper.Map<List<VideosVm>>(videoList);
    }
}
