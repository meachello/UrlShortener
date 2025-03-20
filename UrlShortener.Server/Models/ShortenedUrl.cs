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

public static class UrlShortenerAlgorithm
{
    private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private const int CodeLength = 6;
    private static readonly Random _random = new Random();

    public static string GenerateShortenedUrl()
    {
        var shortUrl = new char[CodeLength];
        for (int i = 0; i < shortUrl.Length; i++)
        {
            shortUrl[i] = Alphabet[_random.Next(Alphabet.Length)];
        }
        return new string(shortUrl);
    }
}