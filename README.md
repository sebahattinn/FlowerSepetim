# FlowerSepetim

FlowerSepetim is a full-stack e-commerce platform built to manage and sell flower products. The solution consists of a RESTful API backend and a modern frontend interface.

## Architecture

The backend follows **Clean Architecture** principles to strictly separate concerns, making the system maintainable and testable. The solution is organized into four distinct layers:

- **Domain:** The core layer containing entities, value objects, and repository interfaces. It depends on nothing and holds the pure business logic.
- **Application:** Orchestrates the business flow. It contains DTOs, Validators, and Service interfaces, acting as a bridge between the core logic and external concerns.
- **Infrastructure:** Implements the interfaces defined in lower layers. It handles database connections, Redis caching, and file systems.
- **API:** The entry point that depends on Application and Infrastructure to serve HTTP requests.

## Development Principles

### Domain-Driven Design (DDD)
The project utilizes DDD patterns to model complex business requirements. The logic is centered around the Domain layer, ensuring that technical details (like the database or cache) do not dictate business rules.

### SOLID & Clean Code
The codebase adheres to SOLID principles to minimize coupling and maximize cohesion. Emphasis is placed on readability and simplicity, avoiding over-engineering while maintaining a robust structure.

## Tech Stack

- **Core:** .NET 9
- **Data Access:** Entity Framework Core, SQL Server
- **Caching:** Redis (StackExchange.Redis) with Fail-Safe implementation
- **Frontend:** Vue.js, Tailwind CSS
- **Validation:** FluentValidation
- **Logging:** Serilog

## Setup

1. Configure connection strings for SQL Server and Redis in `appsettings.json`.
2. Apply migrations to update the database schema.
3. Build and run the API project.
4. Navigate to the frontend directory, install dependencies, and run the development server.
