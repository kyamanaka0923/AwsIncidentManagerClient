using System;
using AwsIncidentManagerClient.Domain.IncidentDomain;
using NUnit.Framework;

namespace AwsIncidentManagerClientTest;

public class IncidentTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TitleToString()
    {
        var dateTime = DateTime.Now;
        var incident = new Incident("aaa", AlertLevel.Critical, dateTime);
        
        Assert.AreEqual($"aaa [{dateTime}]",incident.TitleToString());
    }
}