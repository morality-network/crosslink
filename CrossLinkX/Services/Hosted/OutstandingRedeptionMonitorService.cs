using CrossLinkX.Models;
using CrossLinkX.Models.Enums;
using CrossLinkX.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CrossLinkX.Services.Hosted
{
    public class OutstandingRedeptionMonitorService : BackgroundService, IDisposable
    {
        private TokenService _tokenService;
        private OutstandingRedeptionMonitorOptions _options;
        private ILogger<OutstandingRedeptionMonitorService> _logger;

        private IServiceScopeFactory _serviceScopeFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceScopeFactory">The service scope (from DI)</param>
        public OutstandingRedeptionMonitorService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void SetupServices()
        {
            var scope = _serviceScopeFactory.CreateScope();

            _options = scope.ServiceProvider.GetService<IOptions<OutstandingRedeptionMonitorOptions>>().Value;
            _logger = scope.ServiceProvider.GetService<ILogger<OutstandingRedeptionMonitorService>>();
            _tokenService = scope.ServiceProvider.GetService<TokenService>();
        }

        /// <summary>
        /// This is called on kick-off of applicaition
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Setup the services
            SetupServices();

            // Only run if enabled
            if (_options.Enabled)
            {
                // Start the new thread
                await Task.Factory.StartNew(async () =>
                {
                    while (true)
                    {
                        // Try to exchange fees
                        await ExchangeFees();
                    }
                });
            }
        }

        private async Task ExchangeFees()
        {
            // Setup the services
            SetupServices();

            // Define the wait between checks
            var checkFrequency = FrequencyUtility.ParseFrequency(_options.IntervalFrequency);

            try
            {
                // Check for any 

                // Calculate how long sleep for
                _logger.LogDebug($"{DateTime.UtcNow}|Sleep time for the fee monitor service is: {checkFrequency}");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
            }
            finally
            {
                // Delay until next run (cron job)
                await Task.Delay((int)checkFrequency);
            }
        }
    }
}
