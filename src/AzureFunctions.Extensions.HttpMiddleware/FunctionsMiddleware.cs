using AzureFunctions.Extensions.HttpMiddleware.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;

namespace AzureFunctions.Extensions.HttpMiddleware
{
    /// <summary>
    /// Functions middleware to execute HTTP trigger method
    /// </summary>
    public class FunctionsMiddleware : Application.Abstractions.HttpMiddleware
    {
        private readonly Func<HttpContext, Task<IActionResult>> _execute;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpMiddleware"/> class.
        /// </summary>
        /// <param name="func">The task to be executed.</param>
        public FunctionsMiddleware(Func<HttpContext, Task<IActionResult>> func)
        {
            GuardExtensions.IsNotNull(nameof(func), func);
            _execute = func;
        }

        /// <summary>
        /// Executes the results from ActionContext functions.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Task</returns>
        public override async Task InvokeAsync(HttpContext context)
        {
            IActionResult result = await _execute(context);

            await result.ExecuteResultAsync(new ActionContext(context, new RouteData(), new ActionDescriptor()));
        }
    }
}
