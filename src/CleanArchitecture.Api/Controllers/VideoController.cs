using CleanArchitecture.Application.Features.Shared.Queries;
using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using CleanArchitecture.Application.Features.Videos.Queries.PaginationVideos;
using CleanArchitecture.Application.Features.Videos.Queries.Vms;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using System.Net;

namespace CleanArchitecture.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class VideoController : ControllerBase
{
    private readonly IMediator _mediator;

    public VideoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{username}", Name = "GetVideo")]
    [Authorize]
    [ProducesResponseType(typeof(IEnumerable<VideosVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<VideosVm>>> GetVideosByUsername(string username)
    {
        var query = new GetVideosListQuery(username);

        var videos = await _mediator.Send(query);

        return Ok(videos);
    }

    [Authorize]
    [HttpGet("pagination", Name = "PaginationVideo")]
    [ProducesResponseType(typeof(PaginationVm<VideosWithIncludesVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<VideosVm>>> GetPaginationVideos(
        [FromQuery] PaginationVideosQuery paaginationVideoParams
    )
    {

        var paginationVideo = await _mediator.Send(paaginationVideoParams);

        return Ok(paginationVideo);
    }

}
