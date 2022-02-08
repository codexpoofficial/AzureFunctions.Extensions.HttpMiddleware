using AzureFunctions.Extensions.HttpMiddleware.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AzureFunctions.Extensions.HttpMiddleware.Application.Abstractions
{
    /// <summary>
    /// HttpMiddleware Base class.
    /// </summary>
    public abstract class HttpMiddleware : IHttpMiddleware
    {
        /// <summary>
        /// This constructor helps to stop initliaze derived class.
        /// </summary>
        protected HttpMiddleware(){}

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware to be run.</param>
        protected HttpMiddleware(IHttpMiddleware next) =>  this.Next = next;

        /// <summary>
        /// Gets or sets the next middleware to be executed after this one.
        /// </summary>
        public IHttpMiddleware Next { get; set; }

        /// <summary>
        /// This Method is used to invoke httpcontext and returns task.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>task represents asynchronous operation.</returns>
        public abstract Task InvokeAsync(HttpContext context);
    }
}
