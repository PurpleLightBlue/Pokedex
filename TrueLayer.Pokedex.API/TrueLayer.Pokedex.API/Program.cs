using Lamar;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrueLayer.Pokedex.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var logger = host.Services.GetRequiredService<ILogger<Program>>();

            var containerScanned = host.Services.GetRequiredService<IContainer>().WhatDidIScan();
            var containerContents = host.Services.GetRequiredService<IContainer>().WhatDoIHave(@namespace: "TrueLayer.");

            logger.LogInformation(containerScanned);
            logger.LogInformation(containerContents);

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseLamar()
            .ConfigureContainer<ServiceRegistry>(builder =>
            {
                builder.Scan(x =>
                {
                    x.AssembliesAndExecutablesFromApplicationBaseDirectory(filter => filter.FullName.StartsWith("TrueLayer."));
                    x.TheCallingAssembly();
                    x.RegisterConcreteTypesAgainstTheFirstInterface();
                });
            })

                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureLogging(logging =>
            {
                logging.AddNLog();
                logging.AddConsole();
            });
    }
}
