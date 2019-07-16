using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Bus;
using Common;
using Rebus.Routing.TypeBased;
using SupplyService.Handlers;

namespace SupplyService
{
    public class Program
    {
        public const string connectionString = "mongodb://localhost:27017/supplies-db";

        public static void Main(string[] args)
        {
            using (var activator = new BuiltinHandlerActivator())
            {
                activator.Register(o => new MessageHandler());

                Configure.With(activator)
                    .Transport(o => o.UseRabbitMq("amqp://tutorial:P2ssw0rd@localhost:5672", "SUPPLIERS-QUEUE"))
                    .Start();

                activator.Bus.Subscribe<OrderCreatedMessage>().Wait();

                CreateWebHostBuilder(args, activator.Bus).Build().Run();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args, IBus bus) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(svc => svc.AddSingleton<IBus>(bus))
                .UseUrls("http://192.168.5.167:8081")
                .UseStartup<Startup>();
    }
}
