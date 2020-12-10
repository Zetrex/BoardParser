using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BoardParser.Common.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BoardParser
{
    public class Worker : BackgroundService
    {
        private readonly ISiteParser _parser;
        private readonly ILogger<Worker> _logger;

        public Worker(ISiteParser parser,
            ILogger<Worker> logger, 
            IConfiguration configuration)
        {
            _parser = parser;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Start parcing {0}", _parser.GetSiteName());

            while (!stoppingToken.IsCancellationRequested)
            {
                // TODO: add config file with types of parsing
                // TODO: read file with pages

                //await _parser.ParcePageAsync("https://www.rupostings.com/show?id=157534");
                //var list = await _parser.ParceMainPageAsync();

                // TODO: save xml

                Console.WriteLine("Finish");
                break;

                if (stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Operation was canseled by token");
                    return;
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
