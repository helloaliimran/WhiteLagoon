![image description](CleanArchitecture-v1.png)

# Clean Architecture  
 
### Short History
- Applications that follow the `Dependency Inversion Principle` as well as the `Domain-Driven Design (DDD)` 
principles tend to arrive at a similar architecture.
- This architecture has gone by many names over the years.One of the first names was Hexagonal Architecture, followed by Ports-and-Adapters. 
- More recently, it's been cited as the Onion Architecture or Clean Architecture. 

### Details
- Clean architecture puts the business logic and application model at the center of the application.
- infrastructure and implementation details depend on the Application Core.
- This functionality is achieved by defining abstractions, or interfaces, in the Application Core, which are then implemented by types defined in the Infrastructure layer.

### Application Core 
- Application Core contains 2 layers Domain layer (Business model) and Application layer (interfaces).
- The Application Core holds the business model, which includes entities, services, and interfaces.
- These interfaces include abstractions for operations that will be performed using Infrastructure, such as data access, file system access, network calls, etc.

- Application Core types:
    + Entities (business model classes that are persisted)
    + Aggregates (groups of entities)
    + Interfaces
    + Domain Services
    + Specifications
    + Custom Exceptions and Guard Clauses
    + Domain Events and Handlers

### Infrastructure
   - The Infrastructure project typically includes data access implementations.
   - In a typical ASP.NET Core web application, these implementations include the Entity Framework (EF) DbContext, any EF Core Migration objects that have been defined, and data access implementation classes. The most common way to abstract data access implementation code is through the use of the Repository design pattern.
   - In addition to data access implementations, the Infrastructure project should contain implementations of services that must interact with infrastructure concerns. These services should implement interfaces defined in the Application Core, and so Infrastructure should have a reference to the Application Core project.
   - Infrastructure types: 
      + EF Core types (DbContext, Migration)
      + Data access implementation types (Repositories)
      + Infrastructure-specific services (for example, Logger)
### UI Layer
   - The user interface layer in an ASP.NET Core MVC application is the entry point for the application.
   - This project should reference the Application Core project, and its types should interact with infrastructure strictly through interfaces defined in Application Core.
   - No direct instantiation of or static calls to the Infrastructure layer types should be allowed in the UI layer.

   - UI Layer types :
       + Controllers
       + Custom Filters
       + Custom Middleware
       + Views
       + ViewModels
       + Startup
     
## Repository pattern & Unit of work pattern
Creating a repository class for each entity type could result in a lot of redundant code,
and it could result in partial updates. For example, suppose you have to update two different
entity types as part of the same transaction. If each uses a separate database context instance,
one might succeed and the other might fail. One way to minimize redundant code is to use a 
generic repository, and one way to ensure that all repositories use the same database context
(and thus coordinate all updates) is to use a unit of work class.

The unit of work class serves one purpose: to make sure that when you use multiple repositories,
they share a single database context. That way, when a unit of work is complete you can call the
SaveChanges method on that instance of the context and be assured that all related changes will
be coordinated. All that the class needs is a Save method and a property for each repository.
Each repository property returns a repository instance that has been instantiated using the 
same database context instance as the other repository instances.
