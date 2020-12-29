using AspNetCurlMiddleware.Middleware.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCurlMiddleware.Middleware.Processor
{
    ///<inheritdoc/>
    public class CurlBuilder : IRequestBuilder
    {
        public SaveRequestOptions Options { get; set; }
        public CurlBuilder(SaveRequestOptions options)
        {
            Options = options;
        }
        public virtual async Task<string> BuildRequest(HttpContext context)
        {
            var method = context.Request.Method;
            var port = context.Request.Host.Port.HasValue ? $":{context.Request.Host.Port}" : string.Empty;
            var host = $@"{context.Request.Scheme}://{context.Request.Host.Host}{port}{context.Request.Path}";
            var headersText = SerializeRequestHeaders(context);
            var bodyText = await SerializeRequestBody(context);
            var result = $@"curl '{host}' \{Environment.NewLine}-X {method} \{Environment.NewLine}{headersText}{bodyText}";
            var directory = Path.Combine(Environment.CurrentDirectory, Options.LogPath ?? string.Empty);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            var fileName = GetFileName(context);
            File.WriteAllText(Path.Combine(directory, $"{fileName}.curl"), result);
            return result;
        }
        public virtual string SerializeRequestHeaders(HttpContext context)
        {
            var headers = context.Request.Headers;
            var sbHeaders = new StringBuilder();
            foreach (var header in headers)
            {
                sbHeaders.Append($@"-H '{header.Key}: {header.Value}' \{Environment.NewLine}");
            }
            return sbHeaders.ToString();
        }
        public virtual async Task<string> SerializeRequestBody(HttpContext context)
        {
            var bodyText = await new StreamReader(context.Request.Body)
                                                    .ReadToEndAsync();
            var serializedBody = $@"--data-binary '{bodyText}'  \{Environment.NewLine}--compressed";
            context.Request.Body.Position = 0;
            return serializedBody;
        }
        public virtual string GetFileName(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("X-Correlation-Id", out StringValues fileName))
            {
                return fileName;
            }
            else
            {
                return Guid.NewGuid().ToString();
            }
        }
    }
}
