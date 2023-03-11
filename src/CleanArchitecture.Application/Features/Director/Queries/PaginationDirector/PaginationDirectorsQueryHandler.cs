using AutoMapper;
using MediatR;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Shared.Queries;
using CleanArchitecture.Application.Features.Director.Queries.Vms;
using CleanArchitecture.Application.Specifications.Directors;
using Dom = CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Features.Director.Queries.PaginationDirector;

public class PaginationDirectorsQueryHandler : IRequestHandler<PaginationDirectorsQuery, PaginationVm<DirectorVm>>
{
    private readonly IUnityOfWork _unityOfWork;
    private readonly IMapper _mapper;

    public PaginationDirectorsQueryHandler(IUnityOfWork unityOfWork, IMapper mapper)
    {
        _unityOfWork = unityOfWork;
        _mapper = mapper;
    }

    public async Task<PaginationVm<DirectorVm>> Handle(PaginationDirectorsQuery request, CancellationToken cancellationToken)
    {
        var directorSpecificationParams = new DirectorSpecificationParams
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Search = request.Search,
            Sort = request.Sort,
        };

        var spec = new DirectorSpecification(directorSpecificationParams);
        var directors = await _unityOfWork.Repository<Dom.Director>().GetAllWithSpec(spec);

        var specCount = new DirectorForCountingSpecification(directorSpecificationParams);
        var totalDirectors = await _unityOfWork.Repository<Dom.Director>().CountAsync(specCount);
        var totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalDirectors) / Convert.ToDecimal(request.PageSize)));

        var data = _mapper.Map<IReadOnlyList<Dom.Director>, IReadOnlyList<DirectorVm>>(directors);

        var pagination = new PaginationVm<DirectorVm>
        {
            
            Count = totalDirectors,
            Data = data,
            PageCount= totalPages,
            PageIndex=request.PageIndex,
            PageSize=request.PageSize,
        };

        return pagination;
    }
}
