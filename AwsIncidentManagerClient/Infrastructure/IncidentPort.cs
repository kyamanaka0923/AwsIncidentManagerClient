using Amazon.SSMIncidents;
using Amazon.SSMIncidents.Model;
using AwsIncidentManagerClient.Domain.IncidentDomain;
using AwsIncidentManagerClient.Domain.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AwsIncidentManagerClient.Infrastructure;

/// <summary>
/// AWS インシデントマネージャのインシデントを開始する
/// </summary>
public class IncidentPort : IIncidentPort
{
    private readonly AmazonSSMIncidentsClient _client;
    private readonly ILogger<IncidentPort> _logger;
    private readonly string _responsePlanArn;
    
    public IncidentPort(ILogger<IncidentPort> logger)
    {
        // TODO: ConfigurationはProgram.csで読み込んだ情報を利用するように変更
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _responsePlanArn = config["ResponsePlanArn"] ?? throw new AggregateException("appsettingsの情報が不正です: ResponsePlanArn が存在しません");
        _client = new AmazonSSMIncidentsClient();
        _logger = logger;
    }
    
    public void Send(Incident incident)
    {
        var request = new StartIncidentRequest()
        {
            ResponsePlanArn = _responsePlanArn,
            Impact = (int)incident.Level,
            Title = incident.TitleToString(),
        };
        
        _logger.LogTrace("[Start] Send Incident To AWS");
        _logger.LogTrace($"    Incident Info: title : {incident.TitleToString()}, Impact : {incident.Level.ToString()}");
        try
        {
            var response = _client.StartIncidentAsync(request);
            _logger.LogInformation($"    IncidentRecordArn : {response.Result.IncidentRecordArn}");
            _logger.LogInformation($"    Status : {response.Status}");
        }
        catch (Exception ex)
        {
            _logger.LogError("Cannot Start Incident");
            _logger.LogError(ex, ex.Message);
        }
        _logger.LogTrace("[End] Send Incident To AWS");
    }
}