using AspNetCurlMiddleware.Middleware.Processor;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AspNetCurlMiddleware.Middleware
{
    /// <summary>
    /// Middleware that logs the HttpRequest as curl
    /// </summary>
    public class SaveAsCurlMiddleWare
    {
        /// <summary>
        /// RequestDelegate to invoke the next context
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next"></param>
        public SaveAsCurlMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Pipeline worker method that does the business logic and delegates to next middleware in the pipeline 
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <param name="builder">IRequestBuilder that converts HttpRequest to the respective builder type</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, IRequestBuilder builder)
        {
            try
            {
                context.Request.EnableBuffering();
                await _next(context);
                context.Request.Body.Position = 0;
            }
            finally
            {
                if (builder.Options != null && builder.Options.SaveRequest && builder.Options.StatusCodes.Contains(context.Response.StatusCode))
                {
                    await builder.BuildRequest(context);
                }
            }
        }
    }
}
