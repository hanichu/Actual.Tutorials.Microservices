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
using Rebus.Bus;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Common;

namespace OrderService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var activator = new BuiltinHandlerActivator())
            {
//                activator.Handle<OrderCreatedMessage>(async o => { await Task.Yield(); });

                Configure.With(activator)
                    .Transport(o => o.UseRabbitMq("amqp://tutorial:P2ssw0rd@localhost:5672", "ORDERS-QUEUE"))
                    .Start();

                CreateWebHostBuilder(args, activator.Bus).Build().Run();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args, IBus bus) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(svc => svc.AddSingleton<IBus>(bus))
                .UseUrls("http://192.168.5.167:8080")
                .UseStartup<Startup>();
    }
}
