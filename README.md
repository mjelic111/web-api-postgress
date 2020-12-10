# web-api-postgress
Simple API with postgres database

## Project description
This is simple API for managing customets

## Instructions
- Database
  - setup database with `docker-compose up`
- WebApi
  - update connection string (DefaultConnection) in WebApi/appsettings.json
  - run WebApi `dotnet run --project WebApi/WebApi.csproj`
  - to test it go to: https://localhost:5001/swagger/index.html
- Client app
  - go to folder WebApi/client
  - run `npm start`
  - navigate to http://localhost:3000/

## Tools and framwork
- .Net 5
  - Dependency Injection
- SignalR
  - live update on create/edit
- Ef Core
  - Code first
  - Migrations
  - Generic CRUD repository
  - Pagination
- Docker
  - postgres docker for database
- Client App
  - React
  - Javascript
  - SignalR
