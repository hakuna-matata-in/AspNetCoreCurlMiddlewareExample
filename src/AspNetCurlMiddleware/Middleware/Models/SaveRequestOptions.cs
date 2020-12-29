using System.Collections.Generic;

namespace AspNetCurlMiddleware.Middleware.Models
{
    /// <summary>
    /// Paramerter model for the Request Builder
    /// </summary>
    public class SaveRequestOptions
    {
        /// <summary>
        /// Directory for the requests to be saved
        /// </summary>
        public string LogPath { get; set; }
        /// <summary>
        /// Flag for the request to be saved
        /// </summary>
        public bool SaveRequest { get; set; }
        /// <summary>
        /// List of http status codes for which the requests will be converted and saved
        /// </summary>
        public List<int> StatusCodes { get; set; } = new List<int> { 400, 401, 403, 500, 502, 503 }; //default error codes to be logged
    }
}
