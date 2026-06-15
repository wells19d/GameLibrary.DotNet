using GameLibrary.DotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.DotNet.Data
{
    public class GameLibraryContext : DbContext
    {
        public GameLibraryContext(DbContextOptions<GameLibraryContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
    }
}