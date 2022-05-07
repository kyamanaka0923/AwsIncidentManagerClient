using AwsIncidentManagerClient.Domain.IncidentDomain;
using CommandLine;

namespace AwsIncidentManagerClient;

public class Options
{
    [Option('t', "title", Required = true, HelpText = "Incident Title")]
    public string Title { get; set; }
    
    [Option('l', "level", Required = true, Default = AwsIncidentManagerClient.Domain.IncidentDomain.AlertLevel.High, HelpText = "Incident Level")]
    public AlertLevel AlertLevel { get; set; }
}