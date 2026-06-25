using GameLibrary.DotNet.Data;
using GameLibrary.DotNet.DTOs;
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
        // Supports filtering, sorting, and pagination
        [HttpGet]
        public IActionResult GetGames( // updated from List<Game> GetGames for Metadata building
            string? filterBy, // added for filtering
            string? search, // added for filtering
            bool? earlyAccess, // added for filtering
            int page = 1, // added for pagination
            int pageSize = 20 // added for pagination
            )
        {
            var games = _context.Games.AsQueryable();

            // Now we are going to filter the games based on a case type and a search value
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
                        // YYYY-MM-DD
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

            // Now we are going to order the games by title, removing "The "
            games = games.OrderBy(game =>
                game.Title.StartsWith("The ")
                    ? game.Title.Substring(4)
                    : game.Title
            );

            // Let's protect it so they cannot access page 0
            if (page < 1)
            {
                page = 1;
            }

            // Let's force limit the max the user can call.
            if (pageSize > 50)
            {
                pageSize = 50;
            }

            // Metadata:
            // How many games does this library have?
            var totalGames = games.Count();
            // How many pages do we have?
            var totalPages = (int)Math.Ceiling(totalGames / (double)pageSize);

            // Now, let's paginate the results. By default, page 1 returns the first 20 titles.
            var skipAmount = (page - 1) * pageSize;

            //games = games.Skip(skipAmount).Take(pageSize); // changed for Metadata building.
            var pagedGames = games.Skip(skipAmount).Take(pageSize).ToList();

            // Metadata Example we want
            /*
             {
                "page": 1,
                "pageSize": 2,
                "totalGames": 4,
                "totalPages": 2,
                "games": [
                { "title": "Title 1" },
                { "title": "Title 2" }
                ]}
            */

            //return games.ToList(); // changed for Metadata building
            return Ok(new
            {
                page,
                pageSize,
                totalGames,
                totalPages,
                games = pagedGames
            });
        }

        // This saves a new game
        [HttpPost]

        // Old method accepted Game directly, including Id
        // Replaced with CreateGameDto to control what fields can be submitted
        // public Game CreateGame(Game game)

        public Game CreateGame(CreateGameDto gameDto) // Using updated DTO
        {
            var game = new Game
            {
                Title = gameDto.Title,
                Summary = gameDto.Summary ?? string.Empty,
                Genre = gameDto.Genre,
                Rating = gameDto.Rating,
                Developer = gameDto.Developer ?? string.Empty,
                Publisher = gameDto.Publisher ?? string.Empty,
                Franchise = gameDto.Franchise ?? string.Empty,
                Review = gameDto.Review ?? string.Empty,
                ReleaseDate = gameDto.ReleaseDate,
                EarlyAccess = gameDto.EarlyAccess,
                CoverImage = gameDto.CoverImage ?? string.Empty
            };

            _context.Games.Add(game);
            _context.SaveChanges();

            return game;
        }


        // This updates a game by id.
        // (whole object is needed)
        [HttpPut("{id}")]
        // Old method accepted Game directly, including Id
        // Replaced with UpdateGameDto to control what fields can be submitted
        //public List<Game> UpdateGame(int id, Game updateGame)
        public List<Game> UpdateGame(int id, UpdateGameDTO updateGame) // Using updated DTO
        {
            var game = _context.Games.FirstOrDefault(g => g.Id == id);

            if (game == null)
            {
                return _context.Games.ToList();
            }

            game.Title = updateGame.Title;
            game.Summary = updateGame.Summary ?? string.Empty;
            game.Genre = updateGame.Genre;
            game.Rating = updateGame.Rating;
            game.Developer = updateGame.Developer ?? string.Empty;
            game.Publisher = updateGame.Publisher ?? string.Empty;
            game.Franchise = updateGame.Franchise ?? string.Empty;
            game.Review = updateGame.Review ?? string.Empty;
            game.ReleaseDate = updateGame.ReleaseDate;
            game.EarlyAccess = updateGame.EarlyAccess;
            game.CoverImage = updateGame.CoverImage ?? string.Empty;
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