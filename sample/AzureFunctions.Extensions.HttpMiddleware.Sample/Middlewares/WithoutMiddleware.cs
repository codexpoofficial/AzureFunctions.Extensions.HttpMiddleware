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
    public class WithoutMiddleware : IMiddlewarePipelineFactory
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="httpContextAccessor">HTTP context accessor.</param>
        public WithoutMiddleware(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public IMiddlewarePipeline Add(Func<HttpContext, Task<IActionResult>> func)
        {
            MiddlewarePipeline pipeline = new MiddlewarePipeline(this.httpContextAccessor);

            return pipeline.Use(func);
        }
    }
}
