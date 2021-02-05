using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;

namespace service_a
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting service a...");

            IBus bus = null;
            while (true)
            {
                try
                {
                    bus = RabbitHutch.CreateBus("host=rabbitmq;username=guest;password=guest");
                    break;
                }
                catch (System.Exception)
                {
                    Console.WriteLine("RabbitMQ is not ready yet...");
                    await Task.Delay(1000);
                }
            }
            Console.WriteLine("RabbitMQ is now ready");

            bus.PubSub.Subscribe<string>("service-a", msg => Console.WriteLine(msg));

            // Wait forever
            await Task.Delay(Timeout.Infinite);
        }
    }
}
