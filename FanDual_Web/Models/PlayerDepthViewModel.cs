using Newtonsoft.Json;

namespace FanDual_Web.Models;

public class PlayerDepthViewModel
{
    [JsonProperty(PropertyName = "id")]
    public required int Id { get; set; } 
    
    [JsonProperty(PropertyName = "position_id")]
    public required int PositionId { get; set; } 
    
    [JsonProperty(PropertyName = "position_code")]
    public required string PositionCode { get; set; } 
    
    [JsonProperty(PropertyName = "head_position_code")]
    public required string HeadPositionCode { get; set; } 
    
    [JsonProperty(PropertyName = "head_position_id")]
    public required int HeadPositionId { get; set; } 
    
    [JsonProperty(PropertyName = "player_id")]
    public required int PlayerId { get; set; } 
    
    [JsonProperty(PropertyName = "player_name")]
    public required string PlayerName { get; set; } 
    
    [JsonProperty(PropertyName = "position_depth")]
    public required int PositionDepth { get; set; } 
    
    [JsonProperty(PropertyName = "sport_id")]
    public required int SportId { get; set; } 
    
    [JsonProperty(PropertyName = "sport_name")]
    public required string SportName { get; set; } 
    
    [JsonProperty(PropertyName = "team_id")]
    public required int TeamId { get; set; } 
    
    [JsonProperty(PropertyName = "team_name")]
    public required string TeamNane { get; set; } 
    
}