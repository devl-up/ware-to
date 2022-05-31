using Application.Catalog.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/products")]
public sealed class ProductController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Add([FromServices] IMediator mediator, AddProduct.Command command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}