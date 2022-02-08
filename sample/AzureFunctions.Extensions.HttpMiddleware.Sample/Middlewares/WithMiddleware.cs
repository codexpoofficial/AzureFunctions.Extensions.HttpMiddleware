using AzureFunctions.Extensions.HttpMiddleware.Application.Abstractions;
using AzureFunctions.Extensions.HttpMiddleware.Application.Interfaces;
using AzureFunctions.Extensions.HttpMiddleware.Domain;
using AzureFunctions.Extensions.HttpMiddleware.Domain.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AzureFunctions.Extensions.HttpMiddleware.Sample.Middlewares
{
    /// <summary>
    /// MiddlewarePipelineFactory inherit IMiddlewarePipelineFactory.
    /// </summary>
    public class WithMiddleware : IMiddlewarePipelineFactory
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="httpContextAccessor">HTTP context accessor.</param>
        /// <param name="middlewareA">Middleware A</param>
        /// <param name="middlewareB">Middleware B</param>
        public WithMiddleware(IHttpContextAccessor httpContextAccessor) => this.httpContextAccessor = httpContextAccessor;

        /// <summary>
        /// Creates a pipeline to validate query parameters.
        /// </summary>
        /// <param name="func">context, task</param>
        /// <returns>middleware pipeline</returns>
        public IMiddlewarePipeline Add(Func<HttpContext, Task<IActionResult>> func)
        {
            MiddlewarePipeline pipeline = new MiddlewarePipeline(this.httpContextAccessor);

            // If Function1 is called, then use MiddlewareA and B, else use MiddlewareB only
            return pipeline.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/api/Function1"),
                                     p => p.Use(async context => {
                                         await context.Response.WriteAsync("hello custom middleware");
                                     }));
        }
    }
}
