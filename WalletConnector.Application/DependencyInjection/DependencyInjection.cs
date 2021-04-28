using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using WalletConnector.Application.Common.AutoMapper;
using WalletConnector.Application.Common.Behaviours;

namespace WalletConnector.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, Action<ClientKeys> configureKeys)
        {
            services.AddAutoMapper(typeof(TransactionMapperProfile).Assembly);
            services.AddAutoMapper(typeof(AccountMapperProfile).Assembly);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddOptions<ClientKeys>().Configure(configureKeys);

            return services;
        }
    }
}
