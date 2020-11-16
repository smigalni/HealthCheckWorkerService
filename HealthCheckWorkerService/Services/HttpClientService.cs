using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HealthCheckWorkerService.Services
{
    public class HttpClientService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public HttpClientService(IHttpClientFactory httpClient,
            ILogger<HttpClientService> logger)
        {
            _httpClient = httpClient.CreateClient();
            _logger = logger;
        }
        public async Task ExecuteRequest(CancellationToken cancellationToken)
        {
            var url = "https://sergeydotnet.com";

            var response = await _httpClient
                    .GetAsync(url, cancellationToken);

            if (response.IsSuccessStatusCode == false)
            {
                _logger.LogError($"The response status code in not success. " +
                    $"The code is {response.StatusCode} and the message is {response.ReasonPhrase}." +
                    $" The url is {url}");
            }
        }
    }
}