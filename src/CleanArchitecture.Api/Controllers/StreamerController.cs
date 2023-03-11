using MediatR;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CleanArchitecture.Application.Features.Streamers.Queries.Vms;
using CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer;
using CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer;
using CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;
using CleanArchitecture.Application.Features.Streamers.Queries.GetStreamerListByUsername;
using CleanArchitecture.Application.Features.Streamers.Queries.GetStreamerListByUrlQuery;


namespace CleanArchitecture.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class StreamerController : ControllerBase
{
    private readonly IMediator _mediator;

    public StreamerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("ByUsername/{username}", Name = "GetStreamerByUsername")]
    [ProducesResponseType(typeof(IEnumerable<StreamerVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<StreamerVm>>> GetStreamerByUsername(string username)
    {
        var query = new GetStreamerListQuery(username);
        var streamers = await _mediator.Send(query);
        return Ok(streamers);
    }

    [HttpGet("ByUrl/{url}", Name = "GetStreamerByUrl")]
    [ProducesResponseType(typeof(IEnumerable<StreamerVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<StreamerVm>>> GetStreamerByUrl(string url)
    {
        var query = new GetStreamerListByUrlQuery(url);
        var streamers = await _mediator.Send(query);
        return Ok(streamers);
    }

    [HttpPost(Name = "CreateStreamer")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<int>> CreateStreamer([FromBody] CreateStreamerCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut(Name = "UpdateStreamer")]
    [ProducesDefaultResponseType]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> UpdateStreamer([FromBody] UpdateStreamerCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteStreamer")]
    [ProducesDefaultResponseType]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> DeleteStreamer([FromRoute] int id)
    {
        var command = new DeleteStreamerCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
