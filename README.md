# Sentinel API

A secure RESTful API built with ASP.NET Core Web API and Clean Architecture principles.

This project is focused on showcasing backend engineering practices including authentication, authorization, API security, scalable architecture, and production-style backend development.

## Planned Features

* JWT Authentication
* Refresh Token Rotation
* Role-Based Authorization (RBAC)
* Secure REST API practices
* Rate Limiting
* Audit Logging
* Validation & Exception Handling
* Dockerized Deployment
* Unit Testing
* Clean Architecture

## Tech Stack

* C#
* ASP.NET Core (.NET 10)
* Entity Framework Core
* PostgreSQL
* ASP.NET Identity
* JWT Bearer Authentication
* FluentValidation
* Serilog
* Docker

## Architecture

This project follows Clean Architecture principles with clear separation of concerns:

```txt
src/
├── Sentinel.API
├── Sentinel.Application
├── Sentinel.Domain
└── Sentinel.Infrastructure
```

## Current Progress

* [x] Repository setup
* [x] Clean Architecture setup
* [x] Database configuration
* [ ] JWT Authentication
* [ ] Refresh Tokens
* [ ] RBAC
* [ ] Validation pipeline
* [ ] Audit Logging
* [ ] Rate Limiting
* [ ] Docker support
* [ ] Unit Testing

## Goals

The goal of this project is to build a production-style backend API that showcases secure authentication, scalable architecture, and modern backend engineering practices.
