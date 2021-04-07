using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WalletConnector.Api.Helpers;
using WalletConnector.Application;
using WalletConnector.Application.Filters;
using WalletConnector.Application.Infrastructure.Services.WalletService;
using WalletConnector.Infrastructure;
using WalletConnector.Infrastructure.WalletService;
using WalletConnector.Infrastructure.WalletService.Openway;

namespace WalletConnector.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
                options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseJsonNamingPolicy();
                });
            services.AddApplication();
            services.AddInfrastructure();
            services.AddWalletService(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        
    }

    internal static class CustomExtensionsMethods
    {
        public static IServiceCollection AddWalletService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<WalletServiceConfig>(configuration.GetSection("WalletService"));
            services.AddScoped<IWalletService, OpenwayWalletService>();
            return services;
        }
    }
}
