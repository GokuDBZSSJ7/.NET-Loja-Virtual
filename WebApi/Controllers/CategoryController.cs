using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Shared.Responses;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _service;

    public CategoryController(CategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(ApiResponse<IEnumerable<Category>>.Ok(result));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(ApiResponse<Category>.Ok(result));
    }

    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        await _service.AddAsync(category);
        return Ok(ApiResponse<string>.Ok("Product created"));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Category category)
    {
        if (id != category.Id) return BadRequest(ApiResponse<string>.Fail("ID mismatch"));
        await _service.UpdateAsync(category);
        return Ok(ApiResponse<string>.Ok("Category updated"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok(ApiResponse<string>.Ok("Category deleted"));
    }
}