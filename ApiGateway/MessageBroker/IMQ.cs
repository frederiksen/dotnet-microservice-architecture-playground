using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EasyNetQ;

namespace ApiGateway.MessageBroker
{
    public interface IMQ
    {
        IBus Bus { get; set; }
    }
}
