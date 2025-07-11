using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Shared.Responses;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductService _service;

    public ProductController(ProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(ApiResponse<IEnumerable<Product>>.Ok(result));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(ApiResponse<Product>.Ok(result));
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        await _service.AddAsync(product);
        return Ok(ApiResponse<string>.Ok("Product created"));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Product product)
    {
        if (id != product.Id) return BadRequest(ApiResponse<string>.Fail("ID mismatch"));
        await _service.UpdateAsync(product);
        return Ok(ApiResponse<string>.Ok("Product updated"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok(ApiResponse<string>.Ok("Product deleted"));
    }
}
