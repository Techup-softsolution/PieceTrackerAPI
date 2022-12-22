using PieceTracker.Data;
using PieceTracker.Service;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System;
using PieceTracker.API.Logger;

namespace PieceTracker.API
{
    public static class RegisterService
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            //services.AddScoped<IProjectServices, ProjectServices>();

            Configure(services, DataRegister.GetTypes());
            Configure(services, ServiceRegister.GetTypes());
        }

        private static void Configure(IServiceCollection services, Dictionary<Type, Type> types)
        {
            foreach (var type in types)
                services.AddScoped(type.Key, type.Value);
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}
