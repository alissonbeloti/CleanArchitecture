using AutoMapper;

using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Actors.Queries.Vms;
using CleanArchitecture.Application.Features.Shared.Queries;
using CleanArchitecture.Application.Specifications.Actors;
using CleanArchitecture.Domain;

using MediatR;

namespace CleanArchitecture.Application.Features.Actors.Queries.PaginationActor;

public class PaginationActorQueryHandler : IRequestHandler<PaginationActorQuery, PaginationVm<ActorVm>>
{
    private readonly IUnityOfWork _unityOfWork;
    private readonly IMapper _mapper;

    public PaginationActorQueryHandler(IMapper mapper, IUnityOfWork unityOfWork)
    {
        _mapper = mapper;
        _unityOfWork = unityOfWork;
    }

    public async Task<PaginationVm<ActorVm>> Handle(PaginationActorQuery request, CancellationToken cancellationToken)
    {
        var actorSpecificationParams = new ActorSpecificationParams
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Search = request.Search,
            Sort = request.Sort,
        };

        var spec = new ActorSpecification(actorSpecificationParams);
        var actors = await _unityOfWork.Repository<Actor>().GetAllWithSpec(spec);

        var specCount = new ActorForCountingSpecification(actorSpecificationParams);
        var totalActors = await _unityOfWork.Repository<Actor>().CountAsync(specCount);

        var rounded = Math.Ceiling(Convert.ToDecimal(totalActors)/ Convert.ToDecimal(actorSpecificationParams.PageSize));

        var totalPages = Convert.ToInt32(rounded);

        var data = _mapper.Map<IReadOnlyList<Actor>, IReadOnlyList<ActorVm>>(actors);

        var pargination = new PaginationVm<ActorVm>
        {
            PageIndex = request.PageIndex,
            Count = totalActors,
            PageCount = totalPages,
            Data = data,
            PageSize = request.PageSize,
        };
        return pargination;
    }
}
