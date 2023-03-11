using CleanArchitecture.Application.Features.Director.Commands.CreateDirector;
using CleanArchitecture.Application.Features.Director.Queries.PaginationDirector;
using CleanArchitecture.Application.Features.Director.Queries.Vms;
using CleanArchitecture.Application.Features.Shared.Queries;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Net;

namespace CleanArchitecture.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class DirectorController : ControllerBase
{
    private IMediator _mediator;

    public DirectorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("pagination", Name = "PaginationDirector")]
    [ProducesResponseType(typeof(PaginationVm<DirectorVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginationVm<DirectorVm>>> GetDirector(
        [FromQuery] PaginationDirectorsQuery paginationDirectorQuery)
    {
        var paginationDirector = await _mediator.Send(paginationDirectorQuery);
        return Ok(paginationDirector);
    }

    [HttpPost(Name = "CreateDirector")]
    //[Authorize(Roles = "Administrador")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesDefaultResponseType(typeof(int))]
    public async Task<ActionResult<int>> CreateDirector([FromBody] CreateDirectorCommand command)
    {
        return await _mediator.Send(command);
    }
}
