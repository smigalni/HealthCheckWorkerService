using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HealthCheckWorkerService.Services
{
    public class PhycicalMemoryService
    {
        private readonly ILogger _logger;

        public PhycicalMemoryService(ILogger<PhycicalMemoryService> logger)
        {
            _logger = logger;
        }
        public Task CheckPhysicalMemory()
        {
            var physicalMemoryInUse = PerformanceInfo.GetPhysicalMemoryInUse();

            _logger.LogWarning($"Physical memory in  use is {physicalMemoryInUse}%");
          
            return Task.CompletedTask;
        }
    }
}