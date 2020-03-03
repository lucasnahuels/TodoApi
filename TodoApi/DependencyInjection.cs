using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Interfaces;
using TodoApi.Services;
using TodoApi.Validators;

namespace TodoApi
{
    public static class DependencyInjection
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<ITodoItemsService, TodoItemsService>();
            services.AddScoped<TodoItemValidator>();
        }

    }
}
