using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardParser.Common.Interfaces;
using BoardParser.Common.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BoardParser.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<ISiteParser, RupostingsParserService>();

                    services.AddHostedService<Worker>();
                });
    }
}
