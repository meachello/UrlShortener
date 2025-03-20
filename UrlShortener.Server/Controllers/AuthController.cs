using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UrlShortener.Server.Data;
using UrlShortener.Server.Models;
using UrlShortener.Server.Services;

namespace UrlShortener.Server.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
        
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _authService.AuthenticateAsync(model.Username, model.Password);
            
        if (user == null)
            return Unauthorized(new { message = "Invalid username or password" });
                
        var token = await _authService.GenerateJwtTokenAsync(user);
            
        return Ok(new 
        { 
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            IsAdmin = user.IsAdmin,
            Token = token
        });
    }
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

}