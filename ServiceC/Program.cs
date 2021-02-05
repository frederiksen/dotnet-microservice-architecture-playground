using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using service_c_messages;

namespace service_c
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting service c...");

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
            await Task.Delay(10*1000);

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
