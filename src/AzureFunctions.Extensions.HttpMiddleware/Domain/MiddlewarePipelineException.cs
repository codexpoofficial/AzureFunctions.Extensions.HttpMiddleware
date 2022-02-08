using System;

namespace AzureFunctions.Extensions.HttpMiddleware.Domain
{
    /// <summary>
    /// Capture Middleware Pipeline Exception when exception is thrown while pipeline is misconfigure.
    /// </summary>
    [Serializable]
    public class MiddlewarePipelineException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MiddlewarePipelineException"/> class.
        /// </summary>
        public MiddlewarePipelineException()
            : base("Middleware pipeline must be configured with at least one middleware and the final middleware must return a response")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MiddlewarePipelineException"/> class.
        /// </summary>
        /// <param name="message">The message describing the error.</param>
        public MiddlewarePipelineException(string message)
            : base(message)
        {
        }
    }
}
