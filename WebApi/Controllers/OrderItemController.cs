using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Shared.Responses;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderItemController : ControllerBase
{
    private readonly OrderItemService _service;

    public OrderItemController(OrderItemService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(ApiResponse<IEnumerable<OrderItem>>.Ok(result));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(ApiResponse<OrderItem>.Ok(result));
    }

    [HttpPost]
    public async Task<IActionResult> Create(OrderItem item)
    {
        await _service.AddAsync(item);
        return Ok(ApiResponse<string>.Ok("Order item created"));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, OrderItem item)
    {
        if (id != item.Id) return BadRequest(ApiResponse<string>.Fail("ID mismatch"));
        await _service.UpdateAsync(item);
        return Ok(ApiResponse<string>.Ok("Order item updated"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _service.GetByIdAsync(id);
        await _service.DeleteAsync(item);
        return Ok(ApiResponse<string>.Ok("Order item deleted"));
    }
}
