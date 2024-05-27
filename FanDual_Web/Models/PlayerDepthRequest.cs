using Newtonsoft.Json;

namespace FanDual_Web.Models;

public class PlayerDepthRequestModel
{
    [JsonProperty(PropertyName = "sport_id")]
    public required int SportId { get; set; }

    [JsonProperty(PropertyName = "team_id")]
    public required int TeamId { get; set; }

    [JsonProperty(PropertyName = "position")]
    public required string Position { get; set; }

    [JsonProperty(PropertyName = "player_id")]
    public required int PlayerId { get; set; }

    [JsonProperty(PropertyName = "player_depth")]
    public int PlayerDepth { get; set; }
}