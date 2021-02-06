using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Json;
using EasyNetQ;
using ServiceCMessages;

namespace ServiceC
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting service c...");

            var httpClient = new HttpClient();

            IBus bus = RabbitHutch.CreateBus("host=rabbitmq;username=guest;password=guest");
            // RabbitMQ is ready in 10 sec.
            // TODO: Poll to check when RabbitMQ is ready
            await Task.Delay(10*1000);

            // Setup health check handler
            await bus.Rpc.RespondAsync<ServiceCHealthCheckRequest, ServiceCHealthCheckResponse>(request =>
                new ServiceCHealthCheckResponse()
            );

            // Setup joke handler
            await bus.Rpc.RespondAsync<ServiceCJokeRequest, ServiceCJokeResponse>(async (request) =>
                {
                    try
                    {
                        var result = await httpClient.GetAsync("http://api.icndb.com/jokes/random");

                        result.EnsureSuccessStatusCode(); // throws if not 200-299

                        var jsonString = await result.Content.ReadAsStringAsync();
                        var joke = System.Text.Json.JsonSerializer.Deserialize<Joke>(jsonString);
                        return new ServiceCJokeResponse{ Joke = joke.Value.Joke, Success = true };                
                    }
                    catch (System.Exception)
                    {
                        return new ServiceCJokeResponse{ Success = false };                
                    }
                }
            );

            // Wait forever
            await Task.Delay(Timeout.Infinite);
        }
    }
}
