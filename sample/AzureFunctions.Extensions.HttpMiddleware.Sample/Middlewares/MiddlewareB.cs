using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AzureFunctions.Extensions.HttpMiddleware.Sample.Middlewares
{
    public class MiddlewareB : Application.Abstractions.HttpMiddleware
    {
        public override async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers["x-content"] = "Hello from middleware B";

            if (this.Next != null) await this.Next.InvokeAsync(context);
        }
    }
}
