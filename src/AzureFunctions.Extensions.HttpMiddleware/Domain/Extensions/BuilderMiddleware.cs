using AzureFunctions.Extensions.HttpMiddleware.Application.Interfaces;
using AzureFunctions.Extensions.HttpMiddleware.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace AzureFunctions.Extensions.HttpMiddleware.Domain.Extensions
{
    /// <summary>
    /// Middleware to conditionally execute a branch.
    /// </summary>
    public class BuilderMiddleware : Application.Abstractions.HttpMiddleware
    {
        private readonly IMiddlewarePipeline pipeline;
        private readonly Action<IMiddlewarePipeline> configure;
        private readonly Func<HttpContext, bool> predicate;
        private readonly bool mergePipeline;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuilderMiddleware"/> class.
        /// </summary>
        /// <param name="pipeline">The pipeline. Required.</param>
        /// <param name="predicate">The predicate to evaluate. Required.</param>
        /// <param name="configure">Configures the branch. Optional.</param>
        /// <param name="mergePipeline">Determines if the branch should rejoin the main pipeline or not.</param>
        public BuilderMiddleware(
            IMiddlewarePipeline pipeline,
            Func<HttpContext, bool> predicate,
            Action<IMiddlewarePipeline> configure,
            bool mergePipeline)
        {
            GuardExtensions.IsNotNull(nameof(pipeline), pipeline);
            GuardExtensions.IsNotNull(nameof(predicate), predicate);

            this.pipeline = pipeline;
            this.configure = configure;
            this.predicate = predicate;
            this.mergePipeline = mergePipeline;
        }

        /// <summary>
        /// Runs the middleware.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public override async Task InvokeAsync(HttpContext context)
        {
            if (this.predicate(context))
            {
                // Create new pipeline for branch
                var branch = this.pipeline.New();
                this.configure?.Invoke(branch);

                await branch.RunAsync();

                if (!this.mergePipeline) return;
            }

            if (this.Next != null) await this.Next.InvokeAsync(context);
        }
    }
}
