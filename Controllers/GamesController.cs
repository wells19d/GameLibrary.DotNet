using GameLibrary.DotNet.Data;
using GameLibrary.DotNet.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GameLibrary.DotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly GameLibraryContext _context;

        public GamesController(GameLibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Game> GetGames()
        {
            return _context.Games.ToList();
        }

        [HttpPost]
        public Game CreateGame(Game game)
        {
            _context.Games.Add(game);
            _context.SaveChanges();

            return game;
        }
        [SwaggerOperation(
            Summary = "Fully updates the game",
            Description = "Send the complete Game object. Any fields not provided may be overwritten."
        )]
        [HttpPut("{id}")]
        public List<Game> UpdateGame(int id, Game updateGame)
        {
            var game = _context.Games.FirstOrDefault(g => g.Id == id);

            if (game == null)
            {
                return _context.Games.ToList();
            }

            game.Title = updateGame.Title;
            game.Subtitle = updateGame.Subtitle;
            game.Genre = updateGame.Genre;
            game.Rating = updateGame.Rating;
            game.Review = updateGame.Review;
            game.CoverImage = updateGame.CoverImage;

            _context.SaveChanges();

            return _context.Games.ToList();
        }

        [SwaggerOperation(
            Summary = "Updates only submitted fields",
            Description = "Send only the fields you want to update. Fields not included in the request will stay unchanged. Example: { \"review\": \"New review\" }"
        )]
        [HttpPatch("{id}")]
        public List<Game> PatchGame(int id, Game updateGame)
        {
            var game = _context.Games.FirstOrDefault(g => g.Id == id);

            if (game == null)
            {
                return _context.Games.ToList();
            }

            if (!string.IsNullOrEmpty(updateGame.Title))
                game.Title = updateGame.Title;

            if (!string.IsNullOrEmpty(updateGame.Subtitle))
                game.Subtitle = updateGame.Subtitle;

            if (!string.IsNullOrEmpty(updateGame.Genre))
                game.Genre = updateGame.Genre;

            if (!string.IsNullOrEmpty(updateGame.Review))
                game.Review = updateGame.Review;

            if (!string.IsNullOrEmpty(updateGame.CoverImage))
                game.CoverImage = updateGame.CoverImage;

            if (updateGame.Rating > 0)
                game.Rating = updateGame.Rating;

            _context.SaveChanges();

            return _context.Games.ToList();
        }


        [HttpDelete("{id}")]
        public List<Game> DeleteGame(int id)
        {
            var game = _context.Games.FirstOrDefault(g => g.Id == id);

            if (game == null)
            {
                return _context.Games.ToList();
            }

            _context.Games.Remove(game);
            _context.SaveChanges();

            return _context.Games.ToList();
        }

    }
}