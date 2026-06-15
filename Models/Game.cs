using System.ComponentModel.DataAnnotations;
namespace GameLibrary.DotNet.Models
{
    public class Game
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Genre { get; set; } = string.Empty;

        [Range(1, 10)]
        public int Rating { get; set; }

        public string Developer { get; set; } = string.Empty;

        public string Publisher { get; set; } = string.Empty;

        public string Franchise { get; set; } = string.Empty;

        public string Review { get; set; } = string.Empty;

        public DateTime? ReleaseDate { get; set; }

        public bool EarlyAccess { get; set; }

        public string CoverImage { get; set; } = string.Empty;
    }
}