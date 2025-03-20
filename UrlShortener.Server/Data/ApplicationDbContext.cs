using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using UrlShortener.Server.Models;

namespace UrlShortener.Server.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { Database.EnsureCreated(); }
    
    public DbSet<User> Users { get; set; }
    public DbSet<ShortenedUrl> ShortenedUrls { get; set; }
    public DbSet<About> AboutContent { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShortenedUrl>()
            .HasIndex(u => u.ShortCode)
            .IsUnique();

        modelBuilder.Entity<ShortenedUrl>()
            .HasOne(u => u.CreatedBy)
            .WithMany()
            .HasForeignKey(u => u.CreatedById);

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"),
                Email = "admin@admin.com",
                IsAdmin = true,
                CreatedAt = DateTime.Now,
            },
            new User
                {
                    Id = 2,
                    Username = "user",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("user"),
                    Email = "user@user.com",
                    IsAdmin = false,
                    CreatedAt = DateTime.Now,
                }
        );

        modelBuilder.Entity<About>().HasData(
            new About
            {
                Id = 1,
                Content = "This is a sample about page",
                LastUpdate = DateTime.Now
            }
        );
    }
}