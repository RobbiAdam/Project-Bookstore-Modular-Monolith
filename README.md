# Project Bookstore Modular Monolith

A fictional online bookstore application built with ASP.NET Core using a Modular-Monolith Architecture.

## Table of Contents
- [Overview](#overview)
- [Architecture & Technologies](#architecture--technologies)
  - [Core Technologies](#core-technologies)
  - [Design Patterns & Approaches](#design-patterns--approaches)
  - [Libraries & Frameworks](#libraries--frameworks)
    - [Core Framework](#core-framework)
    - [API Development](#api-development)
    - [Data Access](#data-access)
    - [Dependency Injection](#dependency-injection)
    - [Message Processing](#message-processing)
    - [Monitoring & Logging](#monitoring--logging)
    - [Identity & Security](#identity--security)
- [Project Status](#project-status)
- [Application Architecture Strategy](#application-architecture-strategy)
  - [1. Modular Monolith Architecture](#1-modular-monolith-architecture)
  - [2. Vertical Slice Architecture](#2-vertical-slice-architecture)
  - [3. CQRS Pattern](#3-cqrs-command-query-responsibility-segregation-pattern)
  - [4. Deployment](#4-deployment)
- [How To Run The Project](#how-to-run-the-project)

## Overview
**Note:** This application is a personal project for practice purposes and is currently in progress. Updates may be intermittent.

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


 ## Application Architecture Strategy
 ![Untitled Diagram drawio(1)](https://github.com/user-attachments/assets/3b9b7371-243f-4cec-b8a9-207a288bad83) 
This diagram illustrates the interconnected modular structure, showcasing how different modules interact within the unified application framework while maintaining clear boundaries and responsibilities.

### 1. Modular Monolith Architecture

Unlike traditional monolithic or microservices architectures, this design adopts a modular monolith approach, which offers a strategic balance between architectural complexity and development efficiency:

* Consolidated Codebase: All modules are integrated into a single deployable unit
* Simplified Deployment: Reduced operational complexity compared to microservices
* Enhanced Modularity: Clear separation of concerns within application structure
* Database Consolidation: A single database serving multiple modules

### 2. Vertical Slice Architecture
The application employs vertical slice architecture,to organize code that prioritizes feature-centric for development:

* Feature-Focused Organization: Each module is structured around specific business capabilities
* Minimal Cross-Dependency: Reduces the risk of unintended feature interactions
* Simplified Modification: Developers can modify individual features with minimal system-wide impact
* Improved Code Maintainability: Clear, isolated feature implementations

![Screenshot_98](https://github.com/user-attachments/assets/6cd7611f-30f3-4584-83ce-349bbd69b1e1)


### 3. CQRS (Command Query Responsibility Segregation) Pattern
Integrated alongside vertical slicing, the CQRS pattern split  between the read and write operations:

* Separate Read and Write Models: Distinct models for query and command operations
* Enhanced Performance: Optimized data retrieval and manipulation strategies
* Improved Scalability: Flexible approach to handling complex data interactions
* Alignment with SOLID Principles: Ensures clean, modular, and maintainable code structure

### 4. Deployment

The application is containerized using Docker Desktop, enabling consistent development and production environtments, and portability across different platform / device


## How To Run The Project

you need these tools to able to run this project
* Visual Studio 2022
* .Net 8 (minimum)
* docker desktop


