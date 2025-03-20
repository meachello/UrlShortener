using UrlShortener.Server.Models;

namespace UrlShortener.Server.Services;

public interface IUrlService
{
    Task<ShortenedUrl> CreateShortenedUrlAsync(string url, int userId);
    Task<ShortenedUrl> GetByShortenedUrlAsync(string url);
    Task<ShortenedUrl> GetByIdAsync(int id);
    Task<IEnumerable<ShortenedUrl>> GetAllAsync();
    Task<IEnumerable<ShortenedUrl>> GetByUserIdAsync(int userId);
    Task<bool> DeleteAsync(int id, int userId, bool isAdmin);
    Task<bool> UrlExistsAsync(string originalUrl);
    Task IncrementClicksAsync(string shortCode);
}