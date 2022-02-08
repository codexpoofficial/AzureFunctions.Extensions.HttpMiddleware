using AzureFunctions.Extensions.HttpMiddleware.Application.Interfaces;
using AzureFunctions.Extensions.HttpMiddleware.Domain;
using AzureFunctions.Extensions.HttpMiddleware.Domain.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctions.Extensions.HttpMiddleware.Application.Abstractions
{
    /// <summary>
    /// MiddlewarePipelineFactory
    /// </summary>
    public class MiddlewarePipelineFactory : IMiddlewarePipelineFactory
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;

        protected MiddlewarePipelineFactory(){}

        public MiddlewarePipelineFactory(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public IMiddlewarePipeline Add(Func<HttpContext, Task<IActionResult>> func)
        {
            MiddlewarePipeline pipeline = new MiddlewarePipeline(this._httpContextAccessor);

            return pipeline.Use(func);
        }
    }
}
