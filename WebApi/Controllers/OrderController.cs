using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Shared.Responses;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderService _service;

    public OrderController(OrderService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(ApiResponse<IEnumerable<Order>>.Ok(result));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(ApiResponse<Order>.Ok(result));
    }

    [HttpGet("customer/{customerId}")]
    public async Task<IActionResult> GetByCustomerId(int customerId)
    {
        var result = await _service.GetByCustomerIdAsync(customerId);
        return Ok(ApiResponse<IEnumerable<Order>>.Ok(result));
    }

    [HttpPost]
    public async Task<IActionResult> Create(Order order)
    {
        await _service.AddAsync(order);
        return Ok(ApiResponse<string>.Ok("Order created"));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Order order)
    {
        if (id != order.Id) return BadRequest(ApiResponse<string>.Fail("ID mismatch"));
        await _service.UpdateAsync(order);
        return Ok(ApiResponse<string>.Ok("Order updated"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Order order)
    {
        await _service.DeleteAsync(order);
        return Ok(ApiResponse<string>.Ok("Order deleted"));
    }
}
