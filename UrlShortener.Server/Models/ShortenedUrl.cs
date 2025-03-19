using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Server.Models;

public class ShortenedUrl
{
    public int Id { get; set; }
    
    [Required]
    [Url]
    public string Url { get; set; }
    
    [Required]
    public string ShortCode { get; set; }
    
    public int CreatedById { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public int Clicks { get; set; }
    
    public virtual User CreatedBy { get; set; }
}