using CrossLinkX.Helpers;
using CrossLinkX.Models;
using CrossLinkX.Models.Enums;
using CrossLinkX.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossLinkX
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    webBuilder.ConfigureLogging((hostingcontext, logging) =>
                    {
                        logging.AddConsole();
                        logging.AddDebug();
                        logging.AddEventSourceLogger();
                    });

                    IConfiguration config = new ConfigurationBuilder()
                       .AddEnvironmentVariables() // For the keystore password and changelly key/secret
                       .AddJsonFile("appsettings.json", false, true)
                       .Build();

                    webBuilder.UseConfiguration(config);
                });
    }
}
