# Project Bookstore Modular Monolith

A fictional online bookstore application built with ASP.NET Core using a Modular-Monolith Architecture.
## Overview
**Note:** This application is a personal project for practice purposes and is currently in progress. Updates may be intermittent.

## Table of Contents
- [Overview](#overview)
- [Architecture & Technologies](#architecture--technologies)
  - [Core Technologies](#core-technologies)
  - [Design Patterns & Approaches](#design-patterns--approaches)
  - [Libraries & Frameworks](#libraries--frameworks)
    - [API Development](#api-development)
    - [Data Access](#data-access)
    - [Dependency Injection](#dependency-injection)
    - [Message Processing](#message-processing)
- [Project Status](#project-status)
- [Development](#development)
  - [Prerequisites](#prerequisites)


## Architecture & Technologies

### Core Technologies
* **Web Framework**: ASP.NET Core Web API
* **Architecture**: Modular Monolith with Vertical Slice Architecture
* **Database**: PostgreSQL
* **Message Broker**: RabbitMQ
* **Caching**: Redis
* **Identity Management**: Keycloak
* **Logging & Monitoring**: Serilog, Seq

### Design Patterns & Approaches
* Rich domain models with Domain-Driven Design (DDD)
* Command Query Responsibility Segregation (CQRS)
* Outbox Pattern for handling dual write problems
* Containerization with Docker and docker-compose

## Project Status

| Service | Status |
|---------|--------|
| Catalog Service | ✅ Completed |
| Basket Service | ✅ Completed |
| Ordering Service | ✅ Completed |
| Identity Service | ✅ Completed |
| Testing Container | 🚧 Not Started |

### Libraries & Frameworks

#### Core Framework

 *.NET Core SDK**: Modern, cross-platform development framework

 *Docker & Docker-compose**: Containerization and orchestration tools for deployment

#### API Development

 **Carter**: Minimal API package that allows you to create RESTful applications with a simple, elegant syntax

 **MediatR**: Simple mediator implementation for .NET, facilitating CQRS pattern implementation

 **Mapster**: Fast, flexible object-to-object mapper for .NET

 **FluentValidation**: Library for building strongly-typed validation rules

#### Data Access

 **PostgreSQL**: Open-source relational database
 
 **Microsoft Entity Framework Core**: Modern object-database mapper for .NET
 
 **Redis**: In-memory data structure store, used as cache and message broker

#### Dependency Injection

 **Scrutor**: Assembly scanning and decoration extensions for Microsoft.Extensions.DependencyInjection

#### Message Processing

 **MassTransit**: Distributed application framework for .NET that makes it easy to create applications that use message-based architecture
 
 **RabbitMQ**: Open-source message broker that supports multiple messaging protocols

#### Monitoring & Logging

 **Serilog**: Flexible, structured logging for .NET applications
 
 **Seq**: Centralized logging server that makes it easy to search, analyze, and alert on log data

#### Identity & Security

 **Keycloak**: Open-source Identity and Access Management solution


