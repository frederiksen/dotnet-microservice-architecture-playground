using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;

namespace service_b
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

            bus.PubSub.Subscribe<string>("service-b", msg => Console.WriteLine(msg));

            // Wait forever
            await Task.Delay(Timeout.Infinite);
        }
    }
}
