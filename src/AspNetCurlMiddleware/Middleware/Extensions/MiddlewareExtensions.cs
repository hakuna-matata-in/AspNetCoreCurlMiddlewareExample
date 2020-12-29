using Microsoft.AspNetCore.Builder;

namespace AspNetCurlMiddleware.Middleware.Extensions
{
    /// <summary>
    /// Middleware Extensions
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Adds the save curl as middleware to the pipeline
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSaveAsCurlMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SaveAsCurlMiddleWare>();
        }
    }
}
