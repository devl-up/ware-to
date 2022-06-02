using Application.Spaces.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/spaces")]
public class SpaceController : ControllerBase
{
    public async Task<IActionResult> Add([FromServices] IMediator mediator, AddSpace.Command command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}