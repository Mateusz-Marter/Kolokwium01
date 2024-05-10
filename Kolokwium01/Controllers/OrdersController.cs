using Kolokwium01.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium01.Controllers;

[Route("api")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrdersRepository _repository;
    
    public OrdersController(IOrdersRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("order/{id:int}")]
    public async Task<IActionResult> GetOrder(int id)
    {
        if (!await _repository.DoesOrderExist(id))
        {
            return NotFound($"Order with id: {id} does not exist!");
        }
        
        var order = _repository.GetOrder(id);

        return Ok(order);
    }

    [HttpDelete("client/{id:int")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        if (!await _repository.DoesClientExist(id))
        {
            return NotFound($"Client with id: {id} does not exist!");
        }

        await _repository.DeleteClient(id);

        return Ok();
    }
}

