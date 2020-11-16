using System.Threading;
using System.Threading.Tasks;

namespace HealthCheckWorkerService.Services
{
    public class ApplicationService
    {
        private readonly HttpClientService _httpClientService;
        private readonly PhycicalMemoryService _phycicalMemoryService;
        private readonly CpuService _cpuService;
        private readonly HardDriveService _hardDriveService;

        public ApplicationService(HttpClientService httpClientService,
            PhycicalMemoryService phycicalMemoryService,
            CpuService cpuService,
            HardDriveService hardDriveService)
        {
            _httpClientService = httpClientService;
            _phycicalMemoryService = phycicalMemoryService;
            _cpuService = cpuService;
            _hardDriveService = hardDriveService;
        }
        public async Task Execute(CancellationToken cancellationToken)
        {
            await _httpClientService.ExecuteRequest(cancellationToken);

            await _phycicalMemoryService.CheckPhysicalMemory();

            await _cpuService.CheckCpu();

            await _hardDriveService.CheckDrivesForFreeSpace();
        }      
    }
}