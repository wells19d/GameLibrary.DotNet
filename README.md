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
- CreateGameDto:
  - POST no longer accepts Game directly.
  - POST accepts CreateGameDto.
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

Session 8 – Validation cleanup
Session 9 – Error handling / better status codes
Session 10 – Authentication intro