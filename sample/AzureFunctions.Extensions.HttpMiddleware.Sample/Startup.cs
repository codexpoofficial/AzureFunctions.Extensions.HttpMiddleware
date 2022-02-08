using AzureFunctions.Extensions.HttpMiddleware.Application.Abstractions;
using AzureFunctions.Extensions.HttpMiddleware.Sample.Middlewares;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AzureFunctions.Extensions.HttpMiddleware.Sample.Startup))]

namespace AzureFunctions.Extensions.HttpMiddleware.Sample
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddLogging();            
            builder.Services.AddTransient<Application.Interfaces.IMiddlewarePipelineFactory, WithMiddleware>();
            builder.Services.AddTransient<Application.Interfaces.IMiddlewarePipelineFactory, WithoutMiddleware>();
            builder.Services.AddTransient<MiddlewareA>();
            builder.Services.AddTransient<MiddlewareB>();
            builder.Services.AddTransient<Application.Interfaces.IMiddlewarePipelineFactory, MiddlewarePipelineFactory>();
        }
    }
}
