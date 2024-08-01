## Technologies

* ASP.NET Core 8
* [Entity Framework Core 8](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR)
* [Mapster](https://github.com/MapsterMapper/Mapster/)
* [FluentValidation](https://fluentvalidation.net/)
* [NUnit](https://nunit.org/), [FluentAssertions](https://fluentassertions.com/)
* [FluentMigrator](https://fluentmigrator.github.io/)

## Overview

### API

#### Core
##### Domain

This is the deepest layer and in one of two parts of the Core. This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer. This layer has no dependencies on any other layer or project. 

##### Application

This is the top layer and in one of two parts of the Core. This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. All interfaces must be defined in this layer. This layer does not proceed infrastructure operations. 

#### Infrastructure

This layer contains classes for accessing external resources such as database, external apis, system and so on. These classes should be based on interfaces defined within the application layer. There must not be any interfaces in this layer.

#### Api

This layer is a web api application based on ASP.NET Core 5. This layer depends on both the Application and Infrastructure layers.
The dependency on Infrastructure is only to support dependency injection. Therefore only *Startup.cs* should reference Infrastructure.

### Blazor app
#### App
This is a demo Blazor Standalone Web Assembly App.

#### Application
This layer serves the same purpose as the one in Api project.

#### Infrastructure
This layer serves the same purpose as the one in Api project.

### Rules and Best Practices
1. One method does one single thing. If we need to run a series of actions, use Mediator pattern with MediatR library.
2. Entity Configurations must be in Infrastructure layer and must be separate from the Entity models. This helps in case we do not want to use SQL Server and switch to another database, the Entity Models remain unchanged.
3. Use ServiceResult and ServiceError for standard responses.
4. Any external dependencies and database must be included in Health check response.
5. All services must be in Infrastructure layer and interfaces are defined in Application layer.
6. No more Repository. It is replaced by Persistence in Infrastructure layer if the data source is from Database. If the data source is from a 3rd library, create a folder with the 3rd library name, add interfaces in Application and implement CRUD methods in Infrastructure.
7. 3rd Client libraries must be referenced in Infrastructure, not in Application.
8. Mapster is used over AutoMapper because of performance.
9. Migration runner uses a separate connection with application db context and does not run in the webhost context. 

## Run project locally
### Prerequisites

1. MSSQL Express 2019
2. Microsoft Visual Studio 2022

### API
1. Clone the source code to local folder
2. Update the database ConnectionString.Default in appsettings.json
3. Open the solution and fress F5 to debug API app using IIS Express. The default url is swagger endpoint: https://localhost:44380/swagger
4. Open Web browser and type https://localhost:44380/health for health endpoint

### Blazor app
1. Ensure the API is launched successfully.
2. F5 to run in debug mode.

## References:
1. [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
2. [Mediator Pattern](https://refactoring.guru/design-patterns/mediator)