using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Server.Data;
using UrlShortener.Server.Services;

namespace UrlShortener.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UrlController : ControllerBase
{
    private readonly IUrlService _urlService;
        private readonly IAuthService _authService;
        
        public UrlController(IUrlService urlService, IAuthService authService)
        {
            _urlService = urlService;
            _authService = authService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var urls = await _urlService.GetAllAsync();
            return Ok(urls);
        }
        
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var url = await _urlService.GetByIdAsync(id);
            
            if (url == null)
                return NotFound();
                
            return Ok(url);
        }
        
        [HttpGet("redirect/{shortCode}")]
        public async Task<IActionResult> RedirectToOriginal(string shortCode)
        {
            var url = await _urlService.GetByShortenedUrlAsync(shortCode);
            
            if (url == null)
                return NotFound();
                
            await _urlService.IncrementClicksAsync(shortCode);

            return Redirect(url.Url);
        }
        
        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CreateUrlModel model)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var url = await _urlService.CreateShortenedUrlAsync(model.OriginalUrl, userId);
                return CreatedAtAction(nameof(GetById), new { id = url.Id }, url);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            
            var AdminRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var isAdmin = AdminRole == "Admin";
            
            var result = await _urlService.DeleteAsync(id, userId, isAdmin);
            
            if (!result)
                return NotFound();
                
            return NoContent();
        }
}

public class CreateUrlModel
{
    public string OriginalUrl { get; set; }
}