using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Requests;
using Shared.Responses;
using Application.Helpers;
using Microsoft.Extensions.Configuration;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly CustomerService _customerService;
    private readonly IConfiguration _config;

    public AuthController(CustomerService customerService, IConfiguration config)
    {
        _customerService = customerService;
        _config = config;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var customer = await _customerService.GetByEmailAsync(request.Email);
        var isValid = _customerService.VerifyPassword(customer, request.Password);

        if (!isValid)
            return Unauthorized(ApiResponse<string>.Fail("Credenciais inválidas"));

        var secretKey = _config["Jwt:Secret"];
        if (string.IsNullOrEmpty(secretKey))
            return StatusCode(500, ApiResponse<string>.Fail("Chave JWT não configurada"));

        var token = JwtTokenGenerator.GenerateToken(customer, secretKey);
        return Ok(ApiResponse<string>.Ok(token));
    }
}
