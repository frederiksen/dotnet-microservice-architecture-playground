using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EasyNetQ;
using service_c_messages;

namespace api_gateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
//        public IEnumerable<WeatherForecast> Get()
        public ServiceCTimeResponse Get()
        {
            var bus = RabbitHutch.CreateBus("host=rabbitmq;username=guest;password=guest");
            
            // bus.PubSub.Publish<ServiceCMessage1>(
            //     new ServiceCMessage1 { Property1 = "Hej", Property2 = "med", Property3 = "dig" }
            // );

            var request = new ServiceCTimeRequest {Message = "Hvad er klokken?"};
            var response = bus.Rpc.Request<ServiceCTimeRequest, ServiceCTimeResponse>(request);

            bus.Dispose();

            return response;
        }
    }
}
