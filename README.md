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
