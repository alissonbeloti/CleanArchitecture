using CleanArchitecture.Application.Features.Director.Commands.CreateDirector;

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

    [HttpPost(Name = "CreateDirector")]
    //[Authorize(Roles = "Administrador")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesDefaultResponseType(typeof(int))]
    public async Task<ActionResult<int>> CreateDirector([FromBody] CreateDirectorCommand command)
    {
        return await _mediator.Send(command);
    }
}
