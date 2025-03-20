using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Server.Services;

namespace UrlShortener.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AboutController : ControllerBase
{
    private readonly IAboutService _aboutService;
        
    public AboutController(IAboutService aboutService)
    {
        _aboutService = aboutService;
    }
        
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var about = await _aboutService.GetAboutContentAsync();
        return Ok(about);
    }
        
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update([FromBody] UpdateAboutModel model)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var about = await _aboutService.UpdateAboutContentAsync(model.Content, userId);
        return Ok(about);
    }
}

public class UpdateAboutModel
{
    public string Content { get; set; }
}