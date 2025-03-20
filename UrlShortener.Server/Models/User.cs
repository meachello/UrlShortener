using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Server.Models;

public class User
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Username { get; set; }
    
    [Required]
    public string PasswordHash { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    public bool IsAdmin { get; set; }
    public DateTime CreatedAt { get; set; }

}