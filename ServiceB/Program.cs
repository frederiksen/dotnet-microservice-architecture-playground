using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using ServiceBMessages;

namespace ServiceB
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting service b...");

            IBus bus = RabbitHutch.CreateBus("host=rabbitmq;username=guest;password=guest");
            // RabbitMQ is ready in 10 sec.
            // TODO: Poll to check when RabbitMQ is ready
            await Task.Delay(10*1000);

            // Setup health check handler
            await bus.Rpc.RespondAsync<ServiceBHealthCheckRequest, ServiceBHealthCheckResponse>(request =>
                new ServiceBHealthCheckResponse()
            );

            bus.PubSub.Subscribe<string>("service-b", msg => Console.WriteLine(msg));

            // Wait forever
            await Task.Delay(Timeout.Infinite);
        }
    }
}
