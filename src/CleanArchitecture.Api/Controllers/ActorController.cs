using MediatR;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.Features.Shared.Queries;
using CleanArchitecture.Application.Features.Actors.Queries.Vms;
using CleanArchitecture.Application.Features.Actors.Queries.PaginationActor;

namespace CleanArchitecture.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ActorController : ControllerBase
{
    private IMediator _mediator;

    public ActorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("pagination", Name = "PaginatorActor")]
    [ProducesResponseType(typeof(PaginationVm<ActorVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginationVm<ActorVm>>> GetPaginationActor(
        [FromQuery] PaginationActorQuery paginationActorParams
        )
    {
        var paginationActor = await _mediator.Send(paginationActorParams);
        return Ok(paginationActor);
    }
}
