using MediatR;
using CleanArchitecture.Application.Features.Shared.Queries;
using CleanArchitecture.Application.Features.Director.Queries.Vms;

namespace CleanArchitecture.Application.Features.Director.Queries.PaginationDirector;

public class PaginationDirectorsQuery: PaginationBaseQuery, IRequest<PaginationVm<DirectorVm>>
{ }
