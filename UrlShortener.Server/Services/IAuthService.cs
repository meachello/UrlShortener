using UrlShortener.Server.Models;

namespace UrlShortener.Server.Services;

public interface IAuthService
{
    Task<User> AuthenticateAsync(string username, string password);
    Task<string> GenerateJwtTokenAsync(User user);
    Task<User> GetUserByIdAsync(int id);
    Task<bool> IsAdminAsync(int userId);
}