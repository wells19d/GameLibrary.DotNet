# GameLibrary.DotNet

A personal game library API built with ASP.NET Core, Entity Framework Core, and SQLite.

## Goal
- I'm trying to learn how to build a RESTful API with ASP.NET Core and Entity Framework Core, and I wanted to create a simple project to practice. This API allows users to manage a game collection by performing CRUD operations on game records.

## Features

- Create games
- View all games
- View a single game
- Filter games by title, genre, rating, studio (Publisher, Developer, Franchise), or release date
- Update games
- Delete games

## API Improvements

- Initial sorting by alphabetical order by title
  - Ignores "The " when sorting titles
- Pagination for limiting the number of games returned in a single request
  - Protects against page 0 requests
  - Defaults to 20 games per page
  - Maxes out at 50 games per page
  - Returns metadata page, page size, total games, total pages, and the array of filtered games
- CreateGameDto & UpdateGameDto:
  - POST no longer accepts Game directly.
  - POST accepts CreateGameDto.
  - PUT accepts UpdateGameDto.
  - Controller builds the real Game manually.

## Removals 
- Partially update games (PATCH endpoint)

## Tech Stack

- ASP.NET Core 8
- Entity Framework Core
- SQLite
- Swagger / OpenAPI
- C#

## Game Model

A game contains:

- Title
- Summary
- Genre
- Rating
- Developer
- Publisher
- Franchise
- Review
- Release Date
- Early Access
- Cover Image

## Running the Project

1. Clone the repository
2. Open the solution in Visual Studio 2022
3. Run the project
4. Open Swagger to test the API

## Future Improvements

- Validation
- Authentication
- ~~Search and filtering~~
- ~~Pagination~~
- Azure deployment

## Sessions

### Completed

- Session 1 - Initial project setup
  - Created the ASP.NET Core Web API project. Got Swagger running and tested the default WeatherForecast endpoint.

- Session 2 - Basic model/controller
  - Created a Game model and GamesController with basic CRUD-style endpoints. Started with simple test data before moving to a database.

- Session 3 - Database / EF Core / migrations
  - Added Entity Framework Core and SQLite. Created the GameLibraryContext and added migrations to create the database. Updated the GamesController to use the database instead of temporary test data.

- Session 4 - Search and Filtering
  - Added search and filtering capabilities to the GET /games endpoint. Users can filter games by title, genre, rating, studio (Publisher, Developer, Franchise), release date, or early access.

- Session 5 - Pagination & Sorting
  - Added pagination and sorting to the GET /games endpoint. Users can specify the page number and page size, and results are sorted alphabetically by title while ignoring leading "The ".

- Session 6 - DTOs & Model Binding
  - Added CreateGameDto and UpdateGameDto to the POST and PUT endpoints. The controller now maps DTO fields into the real Game model manually, preventing the request body from controlling Id.

- Session 7 - Validation cleanup
  - Implemented validation for CreateGameDto and UpdateGameDto using data annotations. Added custom validation attributes for specific fields like Rating and ReleaseDate.

- Session 8 - Error handling / better status codes
  - Improved API response handling and status codes.
	- POST now returns  201 Created
	- PUT now returns  200 OK or 404 Not Found
	- DELETE now returns 204 No Content or 404 Not Found.
	- Swagger response types added for clearer API documentation.

- Session 9 - API Key Protection
  - Added local API key protection using .NET user-secrets. 
  - POST, PUT, and DELETE now require an X-API-Key header, returns 401 Unauthorized.
  - Swagger updated with an Authorize button for testing protected endpoints.

### Upcoming

- Session 10 – Frontend planning
- Session 11 – Simple React frontend setup
- Session 12 – Connect frontend to API with dummy API key
- Stretch – Local image handling / cover uploads