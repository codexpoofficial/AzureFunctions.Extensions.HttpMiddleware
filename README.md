<div id=top></div>
<h1 align="center"><a href="https://codexpo.in/" target="blank"><img height="240" src="./assets/logo.png"/><br/>AzureFunctions.Extensions.HttpMiddleware</a></h1>

<p align="center">
  <b>.NET framework introduced isolated process functions,it helps to running out of process in Azure functions. </b>
</p>

<p align="center">  

<a href="https://www.nuget.org/packages/AzureFunctions.Extensions.HttpMiddleware">
<img src="https://img.shields.io/nuget/v/AzureFunctions.Extensions.HttpMiddleware.svg" alt="Middleware version"/>
</a>


<a href="https://www.nuget.org/packages/AzureFunctions.Extensions.HttpMiddleware">
<img src="https://img.shields.io/nuget/dt/AzureFunctions.Extensions.HttpMiddleware.svg" alt="Middleware downloads"/>
</a>

</p>

<br/>

<details>
  <summary>Table of Contents</summary>
  <ol>
	<li><a href="#nugetpackage">NuGet package</a></li>
    <li><a href="#supportedframeworks">Supported Frameworks</a></li>
    <li><a href="#installation">Getting Started</a></li>    
	  <li><a href="#custommiddleware">Add custom middleware</a></li>
    <li><a href="#samples">Samples</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#contributors">Contributors</a></li>
  </ol>
</details>

<a name="nugetpackage" />

## NuGet package

https://www.nuget.org/packages/AzureFunctions.Extensions.HttpMiddleware/1.0.1

<a name="supportedframeworks" />

## Supported Frameworks

 - NetCoreApp 3.1
 - NET 5.0
 - NET 6.0

## Installation

`PM> Install-Package AzureFunctions.Extensions.HttpMiddleware`

### Getting Started

# 1. Add HttpContextAccessor to service collection

Inorder to access/modify HttpContext within custom middleware we need to add HttpContextAccessor in Startup.cs file.

```cs

builder.Services.AddHttpContextAccessor();

```

<a name="custommiddleware" />

# 2. Register custom middlewares to the pipeline

One or more custom middlewares can be added to the execution pipeline using MiddlewareBuilder. 


```cs

public IMiddlewarePipeline Add(Func<HttpContext, Task<IActionResult>> func)
        {
            MiddlewarePipeline pipeline = new MiddlewarePipeline(this.httpContextAccessor);

            // If Function1 is called, then use custom middleware.
            return pipeline.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/api/Function1"),
                                     p => p.Use(async context => {
                                         await context.Response.WriteAsync("hello custom middleware");
                                     }));
        }

```

## 2.1 Use() 

 - Use() middleware takes custom middleware as parameter and will be applied to all the endpoints 

## 2.2 UseWhen()

 - UseWhen() takes Func<HttpContext, bool> and custom middleware as parameters. If the condition is satisfied then middleware will be added to the pipeline 
 of exectuion.

## 2.3 Map() 

 - Map() middleware takes custom middleware as parameter and will be applied to all the endpoints . Map Enables branching pipeline. Runs specified middleware if the condition is met.

## 2.4 MapWhen()

 - MapWhen() takes Func<HttpContext, bool> and custom middleware as parameters. MapWhen also fulfills the same purpose with better control on mapping conditional logic using the predicates, this extension doesn't stop executing the rest of the pipeline when the delegate returns true, which makes "conditional middleware execution. 


# 3. RunAsync pipeline

Now we need to bind last middleware for our HttpTrigger method , to do that wrap our existing code inside Functionsmiddleware block "_middlewareBuilder.ExecuteAsync(new FunctionsMiddleware(async (httpContext) =>{HTTP trigger code})"

```cs
 
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

```

<a name="samples" />

## Sample

You can find .NET 6 sample application [here](https://github.com/Codexpoofficial/AzureFunctions.Extensions.HttpMiddleware/sample) . In this example we have registered Exception handling custom middleware to the exectuion order that
will handle any unhandled exceptions in the Http Trigger execution.


<a name="contributors" />

## Contributors ✨


<table>
  <tr>
    <td align="center"><a href="https://linkedin.com/beingsrvsingh"><img src="https://pbs.twimg.com/profile_images/1441783349832400911/eyYQC5NT_400x400.jpg" width="100px;" alt=""/><br /><sub><b>Saurav Singh</b></sub></a><br /></td>
  </tr>
</table>



## Support

Leave ⭐ if this library helped you at handling custom middleware.

[Website](https://codexpo.in) | [LinkedIn](https://www.linkedin.com/in/beingsrvsingh/) | [Forum](https://github.com/Codexpoofficial/AzureFunctions.Extensions.HttpMiddleware/discussions) | [Contribution Guide](CONTRIBUTING.md) | [License](LICENSE.txt)

&copy; [Saurav Singh](https://github.com/beingsrvsingh)


## Contact

Saurav Singh - [@beingsrvsingh](https://www.linkedin.com/in/beingsrvsingh/) - https://codexpo.in 


## Referances

<a href="https://github.com/umamimolecule/azure-functions-http-middleware">umamimolecule</a>
