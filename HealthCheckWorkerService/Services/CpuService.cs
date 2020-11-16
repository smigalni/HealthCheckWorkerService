using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace HealthCheckWorkerService.Services
{
    public class CpuService
    {
        private readonly ILogger _logger;

        public CpuService(ILogger<CpuService> logger)
        {
            _logger = logger;
        }
        public Task CheckCpu()
        {
            if (OperatingSystem.IsWindows())
            {
                var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total", Environment.MachineName);
                cpuCounter.NextValue();
                Thread.Sleep(100);
                var cpu = (int)cpuCounter.NextValue();

                _logger.LogWarning($"CPU in  use is {cpu}%");
            }
            else
            {
                throw new NotSupportedException();
            }           

            return Task.CompletedTask;
        }
    }
}