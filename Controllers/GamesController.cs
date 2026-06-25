using GameLibrary.DotNet.Data;
using GameLibrary.DotNet.Models;
using Microsoft.AspNetCore.Mvc;

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


        // This gets the games array
        // (by case type and search value)
        [HttpGet]
        public List<Game> GetGames(
            string? filterBy,
            string? search,
            bool? earlyAccess)
        {
            var games = _context.Games.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterBy) && !string.IsNullOrWhiteSpace(search))
            {
                switch (filterBy.ToLower())


                {
                    case "title":
                        // returns by title
                        games = games.Where(game => game.Title.ToLower().Contains(search.ToLower()));
                        break;

                    case "genre":
                        // returns by genre
                        games = games.Where(game => game.Genre.ToLower().Contains(search.ToLower()));
                        break;

                    case "rating":
                        // returns by rating - value is a number
                        if (double.TryParse(search, out double parsedRating))
                        {
                            games = games.Where(game => game.Rating >= parsedRating);
                        }

                        break;

                    case "studio":
                        // returns publisher || developer || franchise
                        games = games.Where(game =>
                            (game.Developer != null &&
                             game.Developer.ToLower().Contains(search.ToLower()))
                            ||
                            (game.Publisher != null &&
                             game.Publisher.ToLower().Contains(search.ToLower()))
                            ||
                            (game.Franchise != null &&
                             game.Franchise.ToLower().Contains(search.ToLower()))
                        );
                        break;

                    case "release":
                        // returns games filtered by date
                        //YYYY-MM-DD
                        if (DateTime.TryParse(search, out DateTime parsedDate))
                        {
                            games = games.Where(game =>
                                game.ReleaseDate.HasValue &&
                                game.ReleaseDate.Value.Date >= parsedDate.Date
                            );
                        }
                        break;
                }
            }

            if (earlyAccess.HasValue)
            {
                games = games.Where(game => game.EarlyAccess == earlyAccess.Value);
            }

            return games.ToList();
        }

        // This saves a new game
        [HttpPost]
        public Game CreateGame(Game game)
        {
            _context.Games.Add(game);
            _context.SaveChanges();

            return game;
        }


        // This updates a game by id.
        // (whole object is needed)
        [HttpPut("{id}")]
        public List<Game> UpdateGame(int id, Game updateGame)
        {
            var game = _context.Games.FirstOrDefault(g => g.Id == id);

            if (game == null)
            {
                return _context.Games.ToList();
            }

            game.Title = updateGame.Title;
            game.Summary = updateGame.Summary;
            game.Genre = updateGame.Genre;
            game.Rating = updateGame.Rating;
            game.Developer = updateGame.Developer;
            game.Publisher = updateGame.Publisher;
            game.Franchise = updateGame.Franchise;
            game.Review = updateGame.Review;
            game.ReleaseDate = updateGame.ReleaseDate;
            game.EarlyAccess = updateGame.EarlyAccess;
            game.CoverImage = updateGame.CoverImage;

            _context.SaveChanges();

            return _context.Games.ToList();
        }

        // Originally had an option that we could just update one field, 
        // but it could cause problems, so we removed PATCH.

        // A simple PATCH implementation cannot distinguish between:
        //   - a field not being sent
        //   - a field intentionally being set to an empty value
        //
        // Since the frontend will already have the full Game object,
        // we use PUT and send the entire object back when updating.

        //[SwaggerOperation(
        //    Summary = "Updates only submitted fields",
        //    Description = "Send only the fields you want to update. Fields not included in the request will stay unchanged. Example: { \"review\": \"New review\" }"
        //)]
        //[HttpPatch("{id}")]
        //public List<Game> PatchGame(int id, Game updateGame)
        //{
        //    var game = _context.Games.FirstOrDefault(g => g.Id == id);

        //    if (game == null)
        //    {
        //        return _context.Games.ToList();
        //    }

        //    if (!string.IsNullOrEmpty(updateGame.Title))
        //        game.Title = updateGame.Title;

        //    if (!string.IsNullOrEmpty(updateGame.Summary))
        //        game.Summary = updateGame.Summary;

        //    if (!string.IsNullOrEmpty(updateGame.Genre))
        //        game.Genre = updateGame.Genre;

        //    if (updateGame.Rating > 0)
        //        game.Rating = updateGame.Rating;

        //    if (!string.IsNullOrEmpty(updateGame.Developer))
        //        game.Developer = updateGame.Developer;

        //    if (!string.IsNullOrEmpty(updateGame.Publisher))
        //        game.Publisher = updateGame.Publisher;

        //    if (!string.IsNullOrEmpty(updateGame.Franchise))
        //        game.Franchise = updateGame.Franchise;

        //    if (!string.IsNullOrEmpty(updateGame.Review))
        //        game.Review = updateGame.Review;

        //    if (updateGame.ReleaseDate.HasValue)
        //        game.ReleaseDate = updateGame.ReleaseDate;

        //    if (updateGame.EarlyAccess != game.EarlyAccess)
        //        game.EarlyAccess = updateGame.EarlyAccess;

        //    if (!string.IsNullOrEmpty(updateGame.CoverImage))
        //        game.CoverImage = updateGame.CoverImage;


        //    _context.SaveChanges();

        //    return _context.Games.ToList();
        //}


        // This deletes a game by id
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