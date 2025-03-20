using UrlShortener.Server.Models;

namespace UrlShortener.Server.Services;

public interface IAboutService
{
    Task<About> GetAboutContentAsync();
    Task<About> UpdateAboutContentAsync(string content, int userId);
}