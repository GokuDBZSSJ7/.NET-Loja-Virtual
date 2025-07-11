using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Shared.Responses;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly CustomerService _service;

    public CustomerController(CustomerService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _service.GetAllAsync();
        return Ok(ApiResponse<IEnumerable<Customer>>.Ok(customers));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _service.GetByIdAsync(id);
        return Ok(ApiResponse<Customer>.Ok(customer));
    }

    [HttpPost]
    public async Task<IActionResult> Create(Customer customer)
    {
        await _service.AddAsync(customer);
        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, ApiResponse<Customer>.Ok(customer));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Customer customer)
    {
        if (id != customer.Id)
            return BadRequest(ApiResponse<string>.Fail("ID in route does not match customer ID"));

        await _service.UpdateAsync(customer);
        return Ok(ApiResponse<Customer>.Ok(customer));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok(ApiResponse<string>.Ok("Customer deleted successfully"));
    }
}
