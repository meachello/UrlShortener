using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using UrlShortener.Server.Data;
using UrlShortener.Server.Models;
using UrlShortener.Server.Services;

namespace UrlShortener.Services
{
    public class AboutService : IAboutService
    {
        private readonly ApplicationDbContext _context;
        
        public AboutService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<About> GetAboutContentAsync()
        {
            return await _context.AboutContent.FirstOrDefaultAsync();
        }
        
        public async Task<About> UpdateAboutContentAsync(string content, int userId)
        {
            var about = await _context.AboutContent.FirstOrDefaultAsync();
            
            if (about == null)
            {
                about = new About
                {
                    Content = content,
                    LastUpdate = DateTime.UtcNow,
                    LastUpdatedById = userId
                };
                _context.AboutContent.Add(about);
            }
            else
            {
                about.Content = content;
                about.LastUpdate = DateTime.UtcNow;
                about.LastUpdatedById = userId;
            }
            
            await _context.SaveChangesAsync();
            return about;
        }
    }
}