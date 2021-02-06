using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EasyNetQ;
using ServiceCMessages;

namespace ApiGateway.Helpers
{
    public class MQ: IMQ
    {
        public IBus Bus { get; set; }

        public MQ()
        {
            Bus = RabbitHutch.CreateBus("host=rabbitmq;username=guest;password=guest");
        }
    }
}
