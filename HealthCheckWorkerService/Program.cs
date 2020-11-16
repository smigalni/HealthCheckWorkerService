using HealthCheckWorkerService;
using HealthCheckWorkerService.Services;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


CreateHostBuilder(args).Build().Run();


static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            services.AddHttpClient();

            services.AddScoped<ApplicationService>();
            services.AddScoped<PhycicalMemoryService>();
            services.AddScoped<CpuService>();
            services.AddScoped<HardDriveService>();
            services.AddScoped<HttpClientService>();

            services.AddTransient<ConfigurationManagerService>();

            services.AddApplicationInsightsTelemetryWorkerService();
            services.AddSingleton<ITelemetryInitializer, TelemetryInitializer>();

            services.AddHostedService<Worker>();
        });

