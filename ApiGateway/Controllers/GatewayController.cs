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
                case "a":
                    _logger.LogInformation("Performing health check for service a");
                    try
                    {
                        var response = _mq.Bus.Rpc.Request<ServiceAHealthCheckRequest, ServiceAHealthCheckResponse>(new ServiceAHealthCheckRequest());
                        return "Ready";
                    }
                    catch (System.Exception)
                    {
                        return "Not ready";
                    }
                case "b":
                    _logger.LogInformation("Performing health check for service b");
                    try
                    {
                        var response = _mq.Bus.Rpc.Request<ServiceBHealthCheckRequest, ServiceBHealthCheckResponse>(new ServiceBHealthCheckRequest());
                        return "Ready";
                    }
                    catch (System.Exception)
                    {
                        return "Not ready";
                    }
                case "c":
                    _logger.LogInformation("Performing health check for service c");
                    try
                    {
                        var response = _mq.Bus.Rpc.Request<ServiceCHealthCheckRequest, ServiceCHealthCheckResponse>(new ServiceCHealthCheckRequest());
                        return "Ready";
                    }
                    catch (System.Exception)
                    {
                        return "Not ready";
                    }
                default:
                    _logger.LogError("Performing health. However the servicename is unknown");
                    return "Unknown service";
            }
        }            

        [HttpGet("os")]
        public string GetOs()
        {        
            return System.Runtime.InteropServices.RuntimeInformation.OSDescription;    
        }

        [HttpGet("joke")]
        public string GetJoke()
        {            
            var request = new ServiceCJokeRequest();
            var response = _mq.Bus.Rpc.Request<ServiceCJokeRequest, ServiceCJokeResponse>(request);
            return response.Joke;
        }
    }
}
