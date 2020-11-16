using Microsoft.Extensions.Configuration;
using System;

namespace HealthCheckWorkerService.Services
{
    public class ConfigurationManagerService
    {
        private readonly IConfiguration _configuration;
        public ConfigurationManagerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int GetTaskDelayTimeInMilliseconds()
        {
            var delayString =
                _configuration.GetValue<string>("TaskDelayTimeInMilliseconds");

            var delayTimeInteger = Convert.ToInt32(delayString);

            if (delayTimeInteger == 0 || delayTimeInteger < 0)
            {
                throw new Exception("Couldn't find value for" +
                    " TaskDelayTimeInMilliseconds in the appsettings.json file");
            }
            return delayTimeInteger;
        }
    }
}