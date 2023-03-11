using CleanArchitecture.Application.Features.Actors.Queries.Vms;
using CleanArchitecture.Application.Features.Shared.Queries;

using MediatR;

namespace CleanArchitecture.Application.Features.Actors.Queries.PaginationActor;

public class PaginationActorQuery: PaginationBaseQuery, IRequest<PaginationVm<ActorVm>>
{

}
