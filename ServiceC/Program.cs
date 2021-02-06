using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using ServiceCMessages;

namespace ServiceC
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting service c...");

            IBus bus = RabbitHutch.CreateBus("host=rabbitmq;username=guest;password=guest");
            // RabbitMQ is ready in 10 sec.
            // TODO: Poll to check when RabbitMQ is ready
            await Task.Delay(10*1000);

            // Setup health check handler
            await bus.Rpc.RespondAsync<ServiceCHealthCheckRequest, ServiceCHealthCheckResponse>(request =>
                new ServiceCHealthCheckResponse()
            );

//            bus.PubSub.Subscribe<ServiceCMessage1>("service-c-message-1", msg => Console.WriteLine(msg));

            var random = new Random();
            await bus.Rpc.RespondAsync<ServiceCTimeRequest, ServiceCTimeResponse>(request =>
                new ServiceCTimeResponse{ Message = "her er svaret: " + random.Next(1000) }
            );

            // Wait forever
            await Task.Delay(Timeout.Infinite);
        }
    }
}
