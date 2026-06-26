using System.ComponentModel.DataAnnotations;

namespace GameLibrary.DotNet.DTOs
{
    public class CreateGameDTO
    {
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "Summary cannot be longer than 500 characters.")]
        public string? Summary { get; set; }

        [Required(ErrorMessage = "Genre is required.")]
        [MaxLength(150, ErrorMessage = "Genre cannot be longer than 150 characters.")]
        public string Genre { get; set; } = string.Empty;

        [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10.")]
        public double Rating { get; set; }

        [MaxLength(100, ErrorMessage = "Developer cannot be longer than 100 characters.")]
        public string? Developer { get; set; }

        [MaxLength(100, ErrorMessage = "Publisher cannot be longer than 100 characters.")]
        public string? Publisher { get; set; }

        [MaxLength(100, ErrorMessage = "Franchise cannot be longer than 100 characters.")]
        public string? Franchise { get; set; }

        [MaxLength(1000, ErrorMessage = "Review cannot be longer than 1000 characters.")]
        public string? Review { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public bool EarlyAccess { get; set; }

        [MaxLength(500, ErrorMessage = "Image URL cannot be longer than 500 characters.")]
        public string? CoverImage { get; set; }
    }
}