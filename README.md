# GameLibrary.DotNet

A personal game library API built with ASP.NET Core, Entity Framework Core, and SQLite.

## Goal
- I'm trying to learn how to build a RESTful API with ASP.NET Core and Entity Framework Core, and I wanted to create a simple project to practice. This API allows users to manage a game collection by performing CRUD operations on game records.

## Features

- Create games
- View all games
- View a single game
- Update games
- Partially update games
- Delete games
- SQLite database persistence
- Swagger API documentation

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
- Search and filtering
- Pagination
- Azure deployment