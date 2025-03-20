using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Server.Models
{
    public class About
    {
        public int Id  { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime LastUpdate { get; set; }
        
        public int? LastUpdatedById { get; set; }

        public virtual User LastUpdatedBy { get; set; }
    }
}
