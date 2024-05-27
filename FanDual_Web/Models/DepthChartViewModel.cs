using Newtonsoft.Json;

namespace FanDual_Web.Models;

public class DepthChartViewModel
{
    [JsonProperty(PropertyName = "sport")]
    public required string Sport { get; set; } 
    [JsonProperty(PropertyName = "team")]
    public required string Team { get; set; }
    [JsonProperty(PropertyName = "position_headers")]
    public required List<HeaderViewModel> PositionHeaders { get; set; }
}

public class HeaderViewModel
{
    [JsonProperty(PropertyName = "code")]
    public required string  Code { get; set; }
    [JsonProperty(PropertyName = "depth_positions")]
    public required Dictionary<string,List<DepthViewModel>> DepthPositions { get; set; }
}

public class DepthViewModel
{
    [JsonProperty(PropertyName = "id")]
    public required int Id { get; set; }
    [JsonProperty(PropertyName = "name")]
    public required string Name { get; set; }
    [JsonProperty(PropertyName = "number")]
    public required int Number { get; set; }
    [JsonProperty(PropertyName = "depth")]
    public required int Depth { get; set; }
    
}