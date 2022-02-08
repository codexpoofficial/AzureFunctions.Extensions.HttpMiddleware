using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AzureFunctions.Extensions.HttpMiddleware.Application.Interfaces
{
    public interface IMiddlewarePipelineFactory
    {
        /// <summary>
        /// This interface allows to pass default params or with params.
        /// </summary>
        /// <param name="func">context, actionresult</param>
        /// <returns>The middleware pipeline.</returns>
        IMiddlewarePipeline Add(Func<HttpContext, Task<IActionResult>> func);
    }
}
