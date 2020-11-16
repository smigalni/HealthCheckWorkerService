using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace HealthCheckWorkerService.Services
{
    public class HardDriveService
    {
        private readonly ILogger _logger;

        public HardDriveService(ILogger<HardDriveService> logger)
        {
            _logger = logger;
        }
        public Task CheckDrivesForFreeSpace()
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (!drive.IsReady)
                {
                    continue;
                }
                double freeSpace = (drive.AvailableFreeSpace / (float)drive.TotalSize) * 100;

                _logger.LogWarning($"Free space on {drive.Name} is {freeSpace}%");
            }
            return Task.CompletedTask;
        }
    }
}