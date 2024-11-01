using FluentValidation;
using fsacwapi.Core.DTOs.UserDTO.Request;
using fsacwapi.Core.Enums;
using fsacwapi.Core.Errors;
using fsacwapi.Core.Models;
using fsacwapi.Infrastructure.Attributes;
using fsacwapi.Infrastructure.Services.JWT;
using fsacwapi.Infrastructure.Services.USER;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace fsacwapi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMemoryCache _memoryCache;
    private readonly UserService _userService;
    private readonly JsonWebTokenService _jsonWebTokenService;

    public AuthController(IMemoryCache memoryCache, UserService userService, JsonWebTokenService jsonWebTokenService)
    {
        _memoryCache = memoryCache;
        _userService = userService;
        _jsonWebTokenService = jsonWebTokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        try
        {
            var result = await _userService.AddUser(request);
            
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            
            return Ok("Registration successful");
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        try
        {
            var (result, user) = await _userService.GetUserByEmail(request.Email);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            
            if (user!.Password != request.Password)
            {
                return Unauthorized(AuthError.LoginException);
            }

            var token = _jsonWebTokenService.GenerateJwtToken(user.Id, user.Role);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }

    [Authorize(Roles = "USER")]
    [HttpGet("user-data/{email}")]
    public async Task<IActionResult> GetUserData(string email)
    {
        try
        {
            var (result, user) = await _userService.GetUserByEmail(email);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            
            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }
}