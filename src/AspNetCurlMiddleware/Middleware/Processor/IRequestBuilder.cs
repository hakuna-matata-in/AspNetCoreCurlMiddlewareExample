using AspNetCurlMiddleware.Middleware.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AspNetCurlMiddleware.Middleware.Processor
{
    /// <summary>
    /// Request Builder that converts the HttpRequest to the respective builder format
    /// </summary>
    public interface IRequestBuilder
    {
        /// <summary>
        /// Parameters to the request builder can be passed via options
        /// </summary>
        SaveRequestOptions Options { get; set; }
        /// <summary>
        /// Converts the HttpRequest
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<string> BuildRequest(HttpContext context);
        /// <summary>
        /// Serializes the headers in the request
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        string SerializeRequestHeaders(HttpContext context);
        /// <summary>
        /// Serializes the body in the request
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<string> SerializeRequestBody(HttpContext context);
        /// <summary>
        /// Gets the filename for the request to be saved
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        string GetFileName(HttpContext context);
    }
}
