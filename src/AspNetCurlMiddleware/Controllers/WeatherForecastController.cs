using AspNetCurlMiddleware.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace AspNetCurlMiddleware.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public IActionResult AdvancedSearch([FromBody] AdvancedSearch request)
        {
            var rng = new Random();
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
            result = result.Where(w => w.Date <= DateTime.Now.AddDays(request.NextNDays) && request.Summaries.Contains(w.Summary)).ToArray();
            return Ok(result);
        }

        [HttpPost("badgateway")]
        public IActionResult SampleError1([FromBody] AdvancedSearch request)
        {
            try
            {
                throw new Exception();
            }
            catch (Exception)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadGateway);
            }
        }

        [HttpPost("badrequest")]
        public IActionResult SampleError2([FromBody] AdvancedSearch request)
        {
            return BadRequest();
        }

        [HttpPost("internalservererror")]
        public IActionResult SampleError3([FromBody] AdvancedSearch request)
        {
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
}
