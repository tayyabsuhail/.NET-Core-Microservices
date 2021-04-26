Microservices based architecture where services are developed on top of .NET core 3.1

- **Catalog Microservice**

  -	ASPNET Core Web API application
  -	REST API principles, CRUD operations
  -	Mongo DB NoSQL database connection on docker
  -	N-Layer implementation with repository pattern
  -	Swagger Open API implementation
  -	Docker file implementation
  
- **Basket Microservice**

  -	ASPNET Core Web API application
  -	REST API principles, CRUD operations
  -	Redis database connection on docker
  -	Redis connection implementation
  -	RabbitMQ trigger event queue when checkout cart
  -	Swagger Open API implementation
  -	Docker file implementation

- **Message Broker**

  - RabbitMQ : Event Bus implementation 
  
- **Ordering Microservice**

  -	ASPNET Core Web API application
  -	Entity Framework Core on SQL Server docker
  -	REST API principles, CRUD operations
  -	Consuming RabbitMQ messages
  -	Clean Architecture implementation with CQRS Pattern
  -	Implementation of MediatR, & AutoMapper
  -	Swagger Open API implementation
  -	Dockerfile implementation

- **Ocelot microservice**

  - Routing to inside microservices
  - Dockerization API gateway

- **Centralized Logging**

  -	Serilog
  -	Elastic Search
  -	Kibana
