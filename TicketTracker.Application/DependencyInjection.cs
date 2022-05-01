global using DDDTW.Specification.CSharp.Builder;
global using MediatR;
global using System.Linq.Expressions;
global using TicketTracker.Application._Common.Models;
global using TicketTracker.Entity;
global using TicketTracker.Entity.DomainEvents;
global using TicketTracker.Entity.Exceptions;
global using TicketTracker.Entity.PrimitiveTypes;
global using TicketTracker.Entity.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TicketTracker.Application._Common.Behaviors;

namespace TicketTracker.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        return services;
    }
}