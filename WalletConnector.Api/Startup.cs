using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WalletConnector.Api.Helpers;
using WalletConnector.Application;
using WalletConnector.Application.DependencyInjection;
using WalletConnector.Infrastructure;

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
            services.AddScoped<ExceptionHandlerHelper>();
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseJsonNamingPolicy();
                });
            services.AddApplication(x => Configuration.Bind("ClientKeys", x));
            services.AddInfrastructure(x => Configuration.Bind("WalletService", x));

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var logger = context.RequestServices.GetRequiredService<ILogger<ExceptionHandlerHelper>>();
                
                await context.Response.WriteAsJsonAsync(
                    new 
                    {
                        error = context.RequestServices.GetRequiredService<ExceptionHandlerHelper>()
                                    .Do(context, context.Features.Get<IExceptionHandlerPathFeature>().Error, logger)
                    });
            }));
            
            app.UseHttpsRedirection();

            

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        
    }

}
