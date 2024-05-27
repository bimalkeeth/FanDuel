using Newtonsoft.Json;

namespace FanDual_Web.Models;

public class PlayerViewModel
{
    [JsonProperty(PropertyName = "id")]
    public required int Id { get; set; }
    [JsonProperty(PropertyName = "name")]
    public required string Name { get; set; }
    [JsonProperty(PropertyName = "number")]
    public required int Number { get; set; }
}