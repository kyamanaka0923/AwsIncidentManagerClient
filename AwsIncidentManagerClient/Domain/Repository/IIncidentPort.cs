using AwsIncidentManagerClient.Domain.IncidentDomain;

namespace AwsIncidentManagerClient.Domain.Repository;

public interface IIncidentPort
{
    /// <summary>
    /// インシデント発行
    /// </summary>
    /// <param name="incident"></param>
    void Send(Incident incident);
}