namespace Interfaces.Entities;

public class DepthChart
{
    public string Team { get; set; }
    public string Sport { get; set; }
    public Dictionary<string, Dictionary<string, List<Players>>> DepthList { get; set; }
}