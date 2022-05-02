global using System.Linq.Expressions;

global using DDDTW.Specification.CSharp.Builder;

global using MediatR;

global using TicketTracker.Application._Common.Models;
global using TicketTracker.Entity;
global using TicketTracker.Entity.DomainEvents;
global using TicketTracker.Entity.Exceptions;
global using TicketTracker.Entity.PrimitiveTypes;
global using TicketTracker.Entity.Repositories;

using System.Reflection;

using MediatR.Pipeline;

using Microsoft.Extensions.DependencyInjection;

using TicketTracker.Application._Common.Behaviors;
using TicketTracker.Infrastructure.DataBase;

namespace TicketTracker.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IRequestPreProcessor<>), typeof(LoggingBehaviour<>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        services.AddTransient(typeof(IMerchantAccountRepository), typeof(MerchantAccountRepository));
        services.AddTransient(typeof(IProjectRepository), typeof(ProjectRepository));

        return services;
    }
}