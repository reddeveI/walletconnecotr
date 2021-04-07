using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using WalletConnector.Infrastructure.WalletService.AutoMapper;

namespace WalletConnector.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });
            return services;
        }
    }
}
