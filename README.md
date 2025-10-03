# CourseMarket

Modern microservices-based e-commerce platform for online courses. This repository demonstrates end-to-end architecture with identity, API gateway, domain-driven ordering, messaging, and a server-rendered web frontend.

## Features

- Identity & Authorization: Duende IdentityServer with OAuth2/OIDC, Resource Owner Password and Client Credentials flows, refresh tokens, role claims
- API Gateway: Ocelot for routing/aggregation, JWT validation, and RFC 8693 Token Exchange
- Microservices: Catalog (MongoDB), Basket (Redis), Discount (PostgreSQL), Order (SQL Server, DDD), PhotoStock, FakePayment
- Messaging: MassTransit + RabbitMQ for event-driven communication (e.g., course-name-changed, create-order)
- Web Application: ASP.NET Core MVC + Razor Views, cookie auth, OpenIdConnect token management, FluentValidation
- Operability: Docker Compose to orchestrate services; Swagger for service contracts

## Architecture Overview

- Frontend (`Frontends/CourseMarket.Web`): MVC, HttpClient + delegating handlers, calls services via the API gateway
- API Gateway (`Gateways/CourseMarket.Gateways.Web`): Ocelot, `TokenExchangeDelegateHandler` for token exchange and JWT validation
- Identity (`IdentityServer/CourseMarket.IdentityServer`): ApiResource/Scope/Client configuration and user management
- Services (`Services/*`): Independent microservices with their own storage and contracts
  - Catalog: MongoDB, JWT-protected REST API
  - Basket: Redis, MassTransit consumer, JWT authorization
  - Discount: PostgreSQL, JWT authorization
  - Order: DDD (Domain, Application, Infrastructure), EF Core + SQL Server, MediatR, MassTransit consumers
  - PhotoStock: File/image operations
  - FakePayment: Simulated payment workflow
- Shared (`Shared/CourseMarket.Shared`): Shared DTOs, messages, and utilities

## Key Technical Decisions and Patterns

- Identity: IdentityServer with `ApiResource`/`ApiScope`/`Client` configuration; ROPC for user flow, Client Credentials for service-to-service access
- Gateway: Ocelot as a single entry point; `TokenExchangeDelegateHandler` to securely call `discount` and `payment` services via token exchange
- DDD (Order Service):
  - Domain: `Order`, `OrderItem`, `Address`, `IAggregateRoot`, `Entity` abstractions
  - Application: AutoMapper profiles, MediatR commands/handlers, message consumers
  - Infrastructure: EF Core, migrations, SQL Server connection, and `OrderDbContext`
- Messaging: MassTransit with RabbitMQ; queues/endpoints for `course-name-changed` and `create-order`
- Data Access: Catalog (MongoDB), Basket (Redis), Discount (PostgreSQL), Order (SQL Server)
- Validation: FluentValidation for view model validation
- HttpClient Factory: Typed service proxies with delegating handlers to inject access tokens

## Technologies

- .NET 5 / ASP.NET Core, MVC/Razor
- Duende IdentityServer, Duende IdentityModel
- Ocelot API Gateway
- MassTransit, RabbitMQ
- Entity Framework Core (SQL Server), MongoDB Driver, Npgsql (PostgreSQL), StackExchange.Redis
- MediatR, AutoMapper, FluentValidation, Swashbuckle (Swagger)
- Docker / Docker Compose

## Service Topology and Databases

- Catalog: MongoDB (`catalogdb`)
- Basket: Redis (`basketdb`)
- Discount: PostgreSQL (`discountdb`)
- Identity & Order: SQL Server (`identitydb`, `orderdb`)
- Messaging: RabbitMQ (`rabbitmq:management`)

## Identity and Authorization

IdentityServer `Config.cs` summary:

- ApiResources: `resource_catalog`, `resource_basket`, `resource_discount`, `resource_order`, `resource_payment`, `resource_photo_stock`, `resource_gatewayweb`
- Scopes: `*_fullpermission`, `gatewayweb_fullpermission`, `IdentityServerConstants.LocalApi`
- Clients:
  - `WebMvcClient` (Client Credentials)
  - `WebMvcClientForUser` (ROPC, offline access, refresh tokens)
  - `TokenExchangeClient` (Token Exchange grant)

Frontend uses Cookie Authentication; service calls use `ClientCredentialTokenHandler` and `ResourceOwnerPasswordTokenHandler` to attach tokens.

## Messaging Examples

- Basket Service: `course-name-changed-event-basket-service` queue, `CourseNameChangedEventConsumer`
- Order Service: `create-order-service` and `course-name-changed-event-order-service` queues, corresponding consumers

## Folder Structure (Overview)

- `Frontends/CourseMarket.Web`: MVC app, `Startup.cs`, service proxies
- `Gateways/CourseMarket.Gateways.Web`: Ocelot configuration, token exchange handler
- `IdentityServer/CourseMarket.IdentityServer`: IdentityServer configs and UI
- `Services/*`: Independent microservices and layered architecture (notably `Order`)
- `Shared/CourseMarket.Shared`: Shared DTOs/messages and helpers

## Run with Docker Compose

Prerequisite: Docker Desktop

```bash
# Run from repository root
docker compose up --build -d
```

Components and ports are built from the docker-compose file. On first run, seed/migrations are applied automatically by the services (e.g., Order API calls `Database.Migrate`).

For local development you can also run services individually (VS/VS Code F5). Update connection strings and `IdentityServerUrl` values in the corresponding `appsettings.Development.json` files.

## Development Notes

- Order API: `MediatR` command handlers, `EF Core` migration pipeline, `DbContext` configuration
- Catalog API: JWT audience validation (`resource_catalog`), AutoMapper, Mongo `DatabaseSettings`
- Basket API: `RedisService` singleton, MassTransit hosted service, audience `resource_basket`
- Frontend: FluentValidation, cookie auth and token management, HttpClient via API Gateway

## Why This Project?

- Demonstrates microservices end-to-end: identity, gateway, messaging, polyglot persistence, DDD, and layered architecture
- Mirrors real-world scenarios: course rename events, basket updates, order creation, and payment workflow

## Roadmap (Optional)

- Observability: Health Checks, OpenTelemetry, centralized logs/metrics
- Resilience: Polly retry, Circuit Breaker
- Deployment: Kubernetes manifests/Helm charts
- UI: Modern SPA (React/Angular/Vue) alongside MVC

---

This repository highlights my professional skills across microservices, identity, and messaging in .NET. Feel free to open issues for questions or feedback.
