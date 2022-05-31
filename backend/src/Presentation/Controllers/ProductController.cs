using Application.Catalog.Commands;
using Application.Catalog.Queries;
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

    [HttpPost("change-information")]
    public async Task<IActionResult> ChangeInformation([FromServices] IMediator mediator, ChangeInformation.Command command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<GetProducts.Result>> Get([FromServices] IMediator mediator, int pageIndex, int pageSize)
    {
        var result = await mediator.Send(new GetProducts.Query(pageIndex, pageSize));
        return Ok(result);
    }
}