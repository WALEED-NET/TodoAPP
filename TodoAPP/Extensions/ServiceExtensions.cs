using System;
using Microsoft.EntityFrameworkCore;
using TodoAPP.Core.Interfaces;
using TodoAPP.Infrastructure.Data;
using TodoAPP.Infrastructure.Services;

namespace TodoAPP.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register Core Services
        services.AddScoped<ITodoService, TodoService>();
        
       
        
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<TodoDbContext>(options =>
            options.UseNpgsql(connectionString));
            

        return services;
    }
}