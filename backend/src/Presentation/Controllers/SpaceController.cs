using Application.Spaces.Commands;
using Application.Spaces.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/spaces")]
public class SpaceController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Add([FromServices] IMediator mediator, AddSpace.Command command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<GetSpaces.Result>> Get([FromServices] IMediator mediator, int pageIndex, int pageSize)
    {
        var result = await mediator.Send(new GetSpaces.Query(pageIndex, pageSize));
        return Ok(result);
    }
}