using HealthCheckWorkerService.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HealthCheckWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ConfigurationManagerService _configurationManagerService;
        private readonly IServiceProvider _services;
        private readonly ILogger _logger;
        private readonly int _delayTime;

        public Worker(
            ConfigurationManagerService configurationManagerService,
            IServiceProvider services,
            ILogger<Worker> logger)
        {
            _configurationManagerService = configurationManagerService;
            _services = services;
            _logger = logger;
            _delayTime = _configurationManagerService.GetTaskDelayTimeInMilliseconds();
        }

        public override async Task StartAsync(
            CancellationToken cancellationToken)
        {
            _logger.LogWarning($"Application {Constants.ApplicationName} started. ");

            await base.StartAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await CreateScopeAndExcecute(cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    //ignore
                }
                catch (Exception exception)
                {
                    _logger.LogError($"Got exception: {exception.Message}. ");
                }                

                await Task.Delay(_delayTime, cancellationToken);
            }
        }

        private async Task CreateScopeAndExcecute(CancellationToken cancellationToken)
        {
            using (var scope = _services.CreateScope())
            {
                var applicationService =
                    scope.ServiceProvider
                        .GetRequiredService<ApplicationService>();

                await applicationService.Execute(cancellationToken);
            }
            GC.Collect();
        }

        public override async Task StopAsync(
          CancellationToken cancellationToken)
        {
            _logger.LogWarning($"Application {Constants.ApplicationName} stopped. ");
            await base.StopAsync(cancellationToken);
        }
    }
}