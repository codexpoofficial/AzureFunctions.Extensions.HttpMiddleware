using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AzureFunctions.Extensions.HttpMiddleware.Application.Interfaces
{
    /// <summary>
    /// Represents middleware pipeline.
    /// </summary>
    public interface IMiddlewarePipeline
    {
        /// <summary>
        /// Adds middleware to the pipeline.
        /// </summary>
        /// <param name="middleware">middleware to add.</param>
        /// <returns>The pipeline.</returns>
        IMiddlewarePipeline Use(IHttpMiddleware middleware);

        /// <summary>
        /// Executes the pipeline.
        /// </summary>
        /// <returns>asynchrous results returned from the Azure function.</returns>
        Task<IActionResult> RunAsync();

        /// <summary>
        /// Executes pipeline
        /// </summary>        
        /// <param name="middleware">HttpMiddleware</param>
        /// <returns>ActionResult task</returns>
        Task<IActionResult> RunAsync(Abstractions.HttpMiddleware middleware);

        /// <summary>
        /// Creates a new derived pipeline instance.
        /// </summary>
        /// <returns>new pipeline.</returns>
        IMiddlewarePipeline New();
    }
}
