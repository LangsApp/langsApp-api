# LangApp
Backend API for a language learning application.

## Technologies
- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- Clean Architecture
- REST API

## Architecture
The project follows Clean Architecture principles:
- Core — domain models and business rules
- BLL — application logic
- DAL — data access
- API — presentation layer

##  How to run locally

### Requirements
- .NET 8 SDK
- PostgreSQL

### Steps
1. Clone repository
2. Configure connection string in `appsettings.Development.json`
3. Apply migrations
4. Run API from Visual Studio or via `dotnet run`

## Tests
Tests are not implemented yet.

## CI/CD
CI will be added later using GitHub Actions.

## Project status
In active development