using AwsIncidentManagerClient;
using AwsIncidentManagerClient.Domain.IncidentDomain;
using AwsIncidentManagerClient.Infrastructure;
using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

Parser.Default.ParseArguments<Options>(args).WithParsed(x =>
{
    var incident = new Incident(x.Title, x.AlertLevel, DateTime.Now);

    var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .Build();

    var serviceProvider = new ServiceCollection()
        .AddTransient<IncidentPort>()
        .AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.SetMinimumLevel(LogLevel.Trace);
            loggingBuilder.AddNLog(config);
        }).BuildServiceProvider();

    var incidentPort = serviceProvider.GetRequiredService<IncidentPort>();

    incidentPort.Send(incident);
});