using System.ComponentModel.DataAnnotations;
namespace GameLibrary.DotNet.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "Summary cannot be longer than 500 characters.")]
        public string Summary { get; set; } = string.Empty;

        [Required(ErrorMessage = "Genre is required.")]
        [MaxLength(150, ErrorMessage = "Genre cannot be longer than 150 characters.")]
        public string Genre { get; set; } = string.Empty;

        [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10.")]
        public double Rating { get; set; }

        [MaxLength(100, ErrorMessage = "Developer cannot be longer than 100 characters.")]
        public string Developer { get; set; } = string.Empty;

        [MaxLength(100, ErrorMessage = "Publisher cannot be longer than 100 characters.")]
        public string Publisher { get; set; } = string.Empty;

        [MaxLength(100, ErrorMessage = "Franchise cannot be longer than 100 characters.")]
        public string Franchise { get; set; } = string.Empty;

        [MaxLength(1000, ErrorMessage = "Review cannot be longer than 1000 characters.")]
        public string Review { get; set; } = string.Empty;

        public DateTime? ReleaseDate { get; set; }

        public bool EarlyAccess { get; set; }

        [MaxLength(500, ErrorMessage = "Image URL cannot be longer than 500 characters.")]
        public string CoverImage { get; set; } = string.Empty;
    }
}