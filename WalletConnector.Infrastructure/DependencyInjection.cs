using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace WalletConnector.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services;
        }
    }
}
