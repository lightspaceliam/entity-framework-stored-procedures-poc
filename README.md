# Entity Framework & Stored Procedures POC

The purpose of this POC is to demonstrate the basics of using stored procedures with .NET Core 5^ & Entity Framework. I have written functionality to provide MVP (Minimum Viable Product) CRUD (Create, Read, Update & Delete) operations, across a Person entity.

Whilst this pattern expands the opportunities for a staggered approach to migrating to a more contemporary data access pattern with Microsoft SQL, my main concerns include:

1. Unit Testing the data access layer
2. Promotes a disconnect from C# to database which facilitates increase work load to make updates to data access layer & database
3. Data loading - navigational properties, EntityFrameworkCore provides this with minimal configuration

## Requirements

- [.NET Core 5.0.7](https://dotnet.microsoft.com/download)
- [SQL Server 2019^ developer](https://www.microsoft.com/en-au/sql-server/sql-server-downloads)
- [Azure Data Studio](https://docs.microsoft.com/en-us/sql/azure-data-studio/download-azure-data-studio?view=sql-server-ver15) or [SQL Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15)
- [Visual Studio or VS Code with appropriate extensions: (SQL Server (mssql))](https://visualstudio.microsoft.com/downloads/)
- Install cli tools, run command from CMD/command prompt:

```cli
dotnet tool install --global dotnet-ef
```

- The current solution is configured to create a local Microsoft SQL database on application start up. It requires access to server: localhost & TCP/IP must be enabled. Feel free to modify connection strings to match your local environment, found here:
    - Api\appsettings.Developement.json
    - Entities\DbContexts\EfStoredProcedureDbContextFactory.cs

## Installation

1. Clone the repository:

```git
git clone https://github.com/lightspaceliam/entity-framework-stored-procedures-poc.git
```

2. With CMD, navigate to: cd {your-directory}/entity-framework-stored-procedures-poc/Entities
3. The Api project now supports auto database migrations. Please review database connections found in:
    - Api\appsettings.Developement.json
    - Entities\DbContexts\EfStoredProcedureDbContextFactory.cs

## Run the Application

Run the application from Visual Studio 2019^, VS Code or even CMD.
Using CMD:
1. With CMD, navigate to: ```cd {your-directory}/entity-framework-stored-procedures-poc/Api```
2. Run command: ```dotnet run```
3. Swagger will load ing the browser at this address: https://localhost:5001/swagger/index.html

## API Endpoints

| Operation | Method | Endpoint |
| --- | --- | --- | 
| **C**reate | POST | /api/people/new
| **R**ead | GET | /api/people
| **U**pdate | PUT | /api​/people​/{id}​/update
| **D**elete | DELETE | /api/people/{id}/delete

## Solution Architecture

This .NET Core solution contains the following:

```
+-- Ef.StoredProcedures
|   +-- Entities
|       +-- DbContexts
|       +-- IEntity.cs
|       +-- Person.cs
|       +-- Scripts
|           +-- Stored Procedures
|           +-- Person Seed
|           +-- Stored Procedures Execute commands
|       +-- Migrations
|           +-- A simple and effective way to deploy the schema, data & Stored Procedures's
|   +-- Entity.Services
|       +-- IEntityService.cs
|       +-- PersonService.cs
|   +-- Entity.Services.Tests
|       +-- PersonServiceTests
|           +-- PersonServiceTestBase.cs reusable boiler plate code for setting up InMemoryDatabase
|           +-- InsertAsyncTests.cs concrete unit tests
|   +-- Api
|       +-- Controllers
|           +-- PersonController.cs CRUD endpoints
```

Whilst I have included a unit testing project with boiler plate in memory database configuration and a single concrete unit test, it appears Microsoft Entity Framework InMemoryDatabase does not support Stored Procedures, well I couldn’t find a simple work around. And that’s an important point! I love the rich toolset .NET Core provides because I can do more with less. I don’t want to or don’t have time to write excessive, overcomplicated code to unit test.

## Pros & Cons

| Pros | Cons |
| --- | --- |
| If the business has a sizable investment in legacy stored procedures, this can be an excellent strategy in migrating across to a more contemporary data access pattern | Does not appear to support in memory unit testing. It’s a sizable but strategic risk
| If the team’s experience with EntityFrameworkCore / LINQ2SQL is limited, then this will facilitate a gradual migration approach | ```.FromSqlRaw("...")``` requires termination via: ```.ToList()``` or ```.ToListAsync()``` even if the successful result returns is a single record
| Migration tasks can be broken down into smaller tasks or units of work | Implementing stored procedures facilitates a detachment from C# entity code and the database
|| If a database table is updated, the developer has to make changes in multiple places
|| More complexity involved to handle concurrency
|| Added complexity to facilitate Eager, Explicit & Lazy loading of related data
|| Added complexity to loading related data. How to handle related entities / navigational properties

## Refereces

- [Raw SQL Queries - Passing parameters](https://docs.microsoft.com/en-us/ef/core/querying/raw-sql#passing-parameters)
- [Entity Framework Core tools reference - .NET Core CLI](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)