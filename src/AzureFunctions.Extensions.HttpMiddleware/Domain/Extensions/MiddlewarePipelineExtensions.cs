using AzureFunctions.Extensions.HttpMiddleware.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AzureFunctions.Extensions.HttpMiddleware.Domain.Extensions
{
    public static class MiddlewarePipelineExtensions
    {
        /// <summary>
        /// Adds an Azure Function middleware to the pipeline.
        /// </summary>
        /// <param name="pipeline">pipeline.</param>
        /// <param name="func">function to add.</param>
        /// <returns>The pipeline instance.</returns>
        public static IMiddlewarePipeline Use(this IMiddlewarePipeline pipeline, Func<HttpContext, Task<IActionResult>> func)
        {
            return pipeline.Use(new FunctionsMiddleware(func));
        }

        /// <summary>
        /// Adds a request delegate middleware to the pipeline.
        /// </summary>
        /// <param name="pipeline">The pipeline.</param>
        /// <param name="requestDelegate">The request delegate to add.</param>
        /// <returns>The pipeline instance.</returns>
        public static IMiddlewarePipeline Use(this IMiddlewarePipeline pipeline, RequestDelegate requestDelegate)
        {
            return pipeline.Use(requestDelegate);
        }

        /// <summary>
        /// UseWhen middleware add when query string matches and added int the request pipeline.
        /// </summary>
        /// <param name="pipeline">The pipeline.</param>
        /// <param name="predicate">The function which is invoked to determine if the branch should be taken.</param>
        /// <param name="configure">Configures the branch.</param>
        /// <returns>The pipeline instance.</returns>
        public static IMiddlewarePipeline UseWhen(
            this IMiddlewarePipeline pipeline,
            Func<HttpContext, bool> predicate,
            Action<IMiddlewarePipeline> configure)
        {
            BuilderMiddleware middleware = new BuilderMiddleware(pipeline, predicate, configure, true);
            return pipeline.Use(middleware);
        }

        /// <summary>
        /// MapWhen middleware add when query string matches and added int the request pipeline.
        /// </summary>
        /// <param name="pipeline">The pipeline.</param>
        /// <param name="predicate">The function which is invoked to determine if the branch should be taken.</param>
        /// <param name="configure">Configures the branch.</param>
        /// <returns>The pipeline instance.</returns>
        public static IMiddlewarePipeline MapWhen(
            this IMiddlewarePipeline pipeline,
            Func<HttpContext, bool> predicate,
            Action<IMiddlewarePipeline> configure)
        {
            BuilderMiddleware middleware = new BuilderMiddleware(pipeline, predicate, configure, false);
            return pipeline.Use(middleware);
        }
    }
}
