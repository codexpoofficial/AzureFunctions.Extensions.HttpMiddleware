using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AzureFunctions.Extensions.HttpMiddleware.Domain
{
    public class HttpResponseResult : IActionResult
    {
        private readonly HttpContext _context;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        public HttpResponseResult(HttpContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Gets the HTTP context.
        /// </summary>
        public HttpContext Context => this._context;

        /// <summary>
        /// This method delivers the 
        /// </summary>
        /// <param name="context"></param>
        /// <returns>asynchronous task operations.</returns>
        public Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext = this._context;
            return Task.CompletedTask;
        }
    }
}
