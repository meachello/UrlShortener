using Microsoft.EntityFrameworkCore;
using UrlShortener.Server.Data;
using UrlShortener.Server.Models;

namespace UrlShortener.Server.Services;

public class UrlService : IUrlService
{
    private readonly ApplicationDbContext _context;

    public UrlService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<ShortenedUrl> CreateShortenedUrlAsync(string originalUrl, int userId)
        {
            if (await UrlExistsAsync(originalUrl))
                throw new InvalidOperationException("URL already exists in the system.");
                
            var shortCode = UrlShortenerAlgorithm.GenerateShortenedUrl();
            while (await _context.ShortenedUrls.AnyAsync(u => u.ShortCode == shortCode))
            {
                shortCode = UrlShortenerAlgorithm.GenerateShortenedUrl();
            }
            
            var shortenedUrl = new ShortenedUrl
            {
                Url = originalUrl,
                ShortCode = shortCode,
                CreatedById = userId,
                CreatedAt = DateTime.UtcNow,
                Clicks = 0
            };
            
            _context.ShortenedUrls.Add(shortenedUrl);
            await _context.SaveChangesAsync();
            
            return shortenedUrl;
        }
        
        public async Task<ShortenedUrl> GetByShortenedUrlAsync(string shortCode)
        {
            return await _context.ShortenedUrls
                .Include(u => u.CreatedBy)
                .FirstOrDefaultAsync(u => u.ShortCode == shortCode);
        }
        
        public async Task<ShortenedUrl> GetByIdAsync(int id)
        {
            return await _context.ShortenedUrls
                .Include(u => u.CreatedBy)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        
        public async Task<IEnumerable<ShortenedUrl>> GetAllAsync()
        {
            return await _context.ShortenedUrls
                .Include(u => u.CreatedBy)
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<ShortenedUrl>> GetByUserIdAsync(int userId)
        {
            return await _context.ShortenedUrls
                .Where(u => u.CreatedById == userId)
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();
        }
        
        public async Task<bool> DeleteAsync(int id, int userId, bool isAdmin)
        {
            var url = await _context.ShortenedUrls.FindAsync(id);
            if (url == null)
                return false;
                
            if (!isAdmin && url.CreatedById != userId)
                return false;
                
            _context.ShortenedUrls.Remove(url);
            await _context.SaveChangesAsync();
            return true;
        }
        
        public async Task<bool> UrlExistsAsync(string originalUrl)
        {
            return await _context.ShortenedUrls.AnyAsync(u => u.Url == originalUrl);
        }
        
        public async Task IncrementClicksAsync(string shortCode)
        {
            var url = await _context.ShortenedUrls.FirstOrDefaultAsync(u => u.ShortCode == shortCode);
            if (url != null)
            {
                url.Clicks++;
                await _context.SaveChangesAsync();
            }
        }
}