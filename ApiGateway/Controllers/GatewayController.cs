using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EasyNetQ;
using ServiceAMessages;
using ServiceBMessages;
using ServiceCMessages;
using ApiGateway.MessageBroker;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/gateway")]
    public class GatewayController : ControllerBase
    {
        private readonly ILogger<GatewayController> _logger;
        private readonly IMQ _mq;

        public GatewayController(ILogger<GatewayController> logger, IMQ mq)
        {
            _logger = logger;
            _mq = mq;
        }

        [HttpGet("healthcheck/{serviceName}")]
        public string GetServiceAHealthCheck(string serviceName)
        {
            switch (serviceName)
            {
                case "servicea":
                    _logger.LogInformation("Performing health check for service a");
                    return "a - ok";
                case "serviceb":
                    _logger.LogInformation("Performing health check for service b");
                    return "b - ok";
                case "servicec":
                    _logger.LogInformation("Performing health check for service c");
                    return "c - ok";
            }
            return "Unknown service";
        }            


        [HttpGet]
//        public IEnumerable<WeatherForecast> Get()
        public ServiceCTimeResponse Get()
        {            
            // bus.PubSub.Publish<ServiceCMessage1>(
            //     new ServiceCMessage1 { Property1 = "Hej", Property2 = "med", Property3 = "dig" }
            // );

            var request = new ServiceCTimeRequest {Message = "Hvad er klokken?"};
            var response = _mq.Bus.Rpc.Request<ServiceCTimeRequest, ServiceCTimeResponse>(request);

            return response;
        }
    }
}
