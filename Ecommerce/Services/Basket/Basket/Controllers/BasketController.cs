using Basket.Commands;
using Basket.Dtos;
using Basket.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IMediator _mediator;
    public BasketController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    // get api/v1/basket/{userName}
    [HttpGet("{userName}")]
    public async Task<ActionResult<ShoppingCartDto>> GetBasketByUserName(string userName)
    {
        var query = new GetBasketByUserNameQuery(userName);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    // post api/v1/basket
    [HttpPost]
    public async Task<ActionResult<ShoppingCartDto>> CreateOrUpdateBasket([FromBody] CreateShoppingCartCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    // delete api/v1/basket/{userName}
    [HttpDelete("{userName}")]
    public async Task<IActionResult> DeleteBasketByUserName(string userName)
    {
        var command = new DeleteBasketByUserNameCommand(userName);
        await _mediator.Send(command);
        return NoContent();
    }
}

