using System.ComponentModel.DataAnnotations;

namespace GameLibrary.DotNet.DTOs
{
    public class UpdateGameDTO
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        public string? Summary { get; set; }

        [Required]
        [MaxLength(100)]
        public string Genre { get; set; } = string.Empty;

        [Range(1, 10)]
        public double Rating { get; set; }

        public string? Developer { get; set; }

        public string? Publisher { get; set; }

        public string? Franchise { get; set; }

        public string? Review { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public bool EarlyAccess { get; set; }

        public string? CoverImage { get; set; }
    }
}