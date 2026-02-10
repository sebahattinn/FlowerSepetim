# FlowerSepetim

FlowerSepetim is a robust, full-stack e-commerce solution designed for managing flower inventory and sales. The system leverages modern .NET 9 features and follows industry-standard architectural patterns to ensure scalability, performance, and maintainability.

## Architecture

The backend is engineered using **Clean Architecture** to strictly separate concerns. This structure minimizes dependencies on external frameworks and places the business logic at the core of the application:

- **Domain:** The heart of the system containing entities and repository interfaces. It is pure C# and has no dependencies on database drivers or external libraries.
- **Application:** Orchestrates business flows using DTOs and Validators. It acts as the bridge between the domain core and the outside world.
- **Infrastructure:** Implements the interfaces defined in lower layers. It manages technical concerns such as EF Core database contexts, Redis caching strategies, and file systems.
- **API:** The entry layer serving RESTful endpoints, relying on the Application and Infrastructure layers to fulfill requests.

## Key Features & Standards

### Domain-Driven Design (DDD)
The project utilizes DDD tactical patterns to model complex business requirements. The architecture ensures that technical implementation details do not dictate business rules.

### Quality Assurance & Testing
Reliability is a priority. The solution includes a comprehensive unit testing suite located in the `CicekSepeti.UnitTests` project.
- **xUnit** is used as the test runner.
- **Moq** is employed to isolate dependencies, ensuring that business logic is tested in isolation without side effects.

### Performance Caching
To optimize response times and reduce database load, a **Cache-Aside** pattern is implemented using **Redis** (via StackExchange.Redis). The system includes a fail-safe mechanism to ensure continuity even if the cache service becomes unavailable.

### API Documentation
All endpoints are fully documented using **Swagger/OpenAPI**. This provides an interactive interface for developers to explore the API capabilities and simplifies frontend integration.  //Ngono Ngolo Kante

## Tech Stack

- **Core:** .NET 9
- **Data Access:** Entity Framework Core, SQL Server
- **Caching:** Redis (Cloud & Local Support)
- **Testing:** xUnit, Moq
- **Documentation:** Swagger (Swashbuckle)
- **Validation:** FluentValidation
- **Frontend:** Vue.js, Tailwind CSS

## Setup

1. Configure the connection strings for SQL Server and Redis in `appsettings.json`.
2. Apply migrations to initialize the database schema.
3. Build and run the API project. The Swagger UI will be available at `/swagger/index.html`.
4. Navigate to the frontend directory, install dependencies, and run the development server.
