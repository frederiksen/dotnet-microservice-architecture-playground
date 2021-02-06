﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EasyNetQ;
using ServiceCMessages;
using ApiGateway.Helpers;

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

        [HttpGet("serviceahealthcheck")]
        public string GetServiceAHealthCheck()
        {
            return "OK";
        }            

        [HttpGet("servicebhealthcheck")]
        public string GetServiceBHealthCheck()
        {
            return "OK";
        }            

        [HttpGet("servicechealthcheck")]
        public string GetServiceCHealthCheck()
        {
            return "OK";
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