﻿global using Carter;
global using Catalog.Contract.Products.Dtos;
global using Catalog.Contract.Products.Features.GetProductByIdQuery;
global using Catalog.Data;
global using Catalog.Products.Events;
global using Catalog.Products.Models;
global using FluentValidation;
global using Mapster;
global using MediatR;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Routing;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Diagnostics;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Shared.Contract.CQRS;
global using Shared.Data.Seed;
global using Shared.DDD;
global using System.Reflection;
