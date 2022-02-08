using AzureFunctions.Extensions.HttpMiddleware.Application.Interfaces;
using AzureFunctions.Extensions.HttpMiddleware.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureFunctions.Extensions.HttpMiddleware.Domain
{
    /// <summary>
    /// Middleware pipeline.
    /// </summary>
    public class MiddlewarePipeline : IMiddlewarePipeline
    {
        private readonly List<IHttpMiddleware> pipeline = new List<IHttpMiddleware>();
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public MiddlewarePipeline(IHttpContextAccessor httpContextAccessor)
        {
            GuardExtensions.IsNotNull(nameof(httpContextAccessor), httpContextAccessor);
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Adds middleware to the pipeline.
        /// </summary>
        /// <param name="middleware">middleware to add.</param>
        /// <returns>The pipeline.</returns>
        public IMiddlewarePipeline Use(IHttpMiddleware middleware)
        {
            if (this.pipeline?.Count > 0) this.pipeline.Last().Next = middleware;

            this.pipeline.Add(middleware);

            return this;
        }

        /// <summary>
        /// Executes the pipeline.
        /// </summary>
        /// <returns>returns http response.</returns>
        public async Task<IActionResult> RunAsync()
        {
            var context = this.httpContextAccessor.HttpContext;

            if (this.pipeline?.Count > 0) await this.pipeline.First().InvokeAsync(context);

            else throw new MiddlewarePipelineException($"No middleware configured");

            return new HttpResponseResult(context);
        }

        /// <summary>
        /// Executes the pipeline.
        /// </summary>
        /// <returns>returns http response.</returns>
        public async Task<IActionResult> RunAsync(Application.Abstractions.HttpMiddleware middleware)
        {
            Use(middleware);

            var context = this.httpContextAccessor.HttpContext;

            if (this.pipeline?.Count > 0)
            {
                await this.pipeline.First().InvokeAsync(context);
                if (context.Response != null) return new HttpResponseResult(context);
            }

            else throw new MiddlewarePipelineException($"No middleware configured");

            return new HttpResponseResult(context);
        }

        /// <summary>
        /// Creates a new pipeline with the same configuration as the current instance.
        /// </summary>
        /// <returns>The new pipeline.</returns>
        public IMiddlewarePipeline New()
        {
            return new MiddlewarePipeline(this.httpContextAccessor);
        }
    }
}
