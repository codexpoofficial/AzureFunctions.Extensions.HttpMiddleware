using AzureFunctions.Extensions.HttpMiddleware.Application.Abstractions;
using AzureFunctions.Extensions.HttpMiddleware.Sample.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AzureFunctions.Extensions.HttpMiddleware.Sample
{
    public class Functions
    {
        private readonly Application.Interfaces.IMiddlewarePipelineFactory _pipelineFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<Functions> _logger;

        public Functions(Application.Interfaces.IMiddlewarePipelineFactory pipelineFactory, IHttpContextAccessor context, ILogger<Functions> logger)
        {
            this._pipelineFactory = pipelineFactory; //.FirstOrDefault(t => t.GetType() == typeof(WithMiddleware));
            this._httpContextAccessor = context;
            this._logger = logger;            
        }

        [FunctionName("Function1")]
        public async Task<IActionResult> Function1(
           [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            return await _pipelineFactory.Add(this.ExecuteFunction).RunAsync();
        }

        [FunctionName("Function2")]
        public async Task<IActionResult> Function2(
           [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {          
            return await _pipelineFactory.Add(async (context) =>
            {
                this._logger.LogInformation("C# HTTP trigger function processed a request.");

                string name = context.Request.Query["name"];

                string requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                name = name ?? data?.name;

                string responseMessage = string.IsNullOrEmpty(name)
                    ? "This HTTP triggered function executed successfully."
                    : $"Hello, {name}. This HTTP triggered function executed successfully.";

                return new OkObjectResult(responseMessage);
            }).RunAsync();
        }

        /// <summary>
        /// Executes the function 1 functions.
        /// </summary>
        /// <param name="context">The HTTP context for the request.</param>
        /// <returns>The <see cref="IActionResult"/> result.</returns>
        private async Task<IActionResult> ExecuteFunction(HttpContext context)
        {
            this._logger.LogInformation("C# HTTP trigger function processed a request.");

            string name = context.Request.Query["name"];

            string requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
