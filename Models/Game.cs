namespace GameLibrary.DotNet.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Subtitle { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string Review { get; set; } = string.Empty;
        public string CoverImage { get; set; } = string.Empty;
    }
}