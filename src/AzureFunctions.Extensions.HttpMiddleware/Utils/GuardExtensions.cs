using System;

namespace AzureFunctions.Extensions.HttpMiddleware.Utils
{
    internal static class GuardExtensions
    {
        /// <summary>
        /// Validates a parameter value is null.
        /// </summary>
        /// <param name="paramName">The parameter name.</param>
        /// <param name="value">The parameter value.</param>
        public static void IsNotNull(string paramName, object value)
        {
            if (value is null) throw new ArgumentNullException(paramName);
        }
    }
}
