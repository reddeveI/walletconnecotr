using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WalletConnector.Application.Infrastructure.Services.WalletService;
using WalletConnector.Infrastructure.WalletService;
using WalletConnector.Infrastructure.WalletService.Openway;


namespace WalletConnector.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, Action<WalletServiceConfig> configure)
        {
            services.AddOptions<WalletServiceConfig>().Configure(configure);
            services.AddScoped<IWalletService, OpenwayWalletService>();
            return services;
        }
    }
}
