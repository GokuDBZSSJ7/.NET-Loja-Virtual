using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Shared.Responses;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CouponController : ControllerBase
{
    private readonly CouponService _service;

    public CouponController(CouponService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var coupons = await _service.GetAllAsync();
        return Ok(ApiResponse<IEnumerable<Coupon>>.Ok(coupons));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var coupon = await _service.GetByIdAsync(id);
        return Ok(ApiResponse<Coupon>.Ok(coupon));
    }

    [HttpGet("code/{code}")]
    public async Task<IActionResult> GetByCode(string code)
    {
        var coupon = await _service.GetByCodeAsync(code);
        return Ok(ApiResponse<Coupon>.Ok(coupon));
    }

    [HttpPost]
    public async Task<IActionResult> Create(Coupon coupon)
    {
        await _service.AddAsync(coupon);
        return CreatedAtAction(nameof(GetById), new { id = coupon.Id }, ApiResponse<Coupon>.Ok(coupon));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Coupon coupon)
    {
        if (id != coupon.Id)
            return BadRequest(ApiResponse<string>.Fail("ID da rota não corresponde ao ID do cupom"));

        await _service.UpdateAsync(coupon);
        return Ok(ApiResponse<Coupon>.Ok(coupon));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok(ApiResponse<string>.Ok("Cupom deletado com sucesso"));
    }

    [HttpGet("calculate-discount")]
    public async Task<IActionResult> CalculateDiscount([FromQuery] string code, [FromQuery] decimal total)
    {
        var discount = await _service.CalculateDiscountAsync(code, total);
        return Ok(ApiResponse<decimal>.Ok(discount));
    }
}
