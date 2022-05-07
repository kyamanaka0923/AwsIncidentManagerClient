namespace AwsIncidentManagerClient.Domain.IncidentDomain;

public class Incident
{
    private readonly string Title;
    public AlertLevel Level { get; }
    private readonly DateTime OccuredDateTime;

    public Incident(string title, AlertLevel level, DateTime occuredDateTime)
    {
        Title = title;
        Level = level;
        OccuredDateTime = occuredDateTime;
    }

    public string TitleToString() => $"{Title} [{OccuredDateTime}]";
}