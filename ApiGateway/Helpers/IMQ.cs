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
    public interface IMQ
    {
        IBus Bus { get; set; }
    }
}
