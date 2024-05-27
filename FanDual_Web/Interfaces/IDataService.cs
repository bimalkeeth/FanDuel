using FanDual_Web.Models;

namespace FanDual_Web.Interfaces;

public interface IDataService
{
    /// <summary>
    /// Adds a player to the depth chart.
    /// </summary>
    /// <param name="positionCode">The code representing the position.</param>
    /// <param name="playerId">The ID of the player to add.</param>
    /// <param name="positionDepth">The depth at which to add the player.</param>
    /// <param name="teamId">The ID of the team.</param>
    /// <param name="sportId">The ID of the sport.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.
    /// The task result is a boolean indicating whether the player was added successfully.</returns>
    public Task<bool> AddPlayerToChartAsync(
        string positionCode,
        int playerId, int positionDepth, int teamId, int sportId);

    /// <summary>
    /// Removes a play from the depth chart.
    /// </summary>
    /// <param name="positionCode">The code representing the position.</param>
    /// <param name="playerId">The ID of the player to remove.</param>
    /// <param name="teamId">The ID of the team.</param>
    /// <param name="sportId">The ID of the sport.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result is a
    /// <see cref="PlayerDepthViewModel"/> object representing the updated player depth information.</returns>
    public Task<PlayerDepthViewModel> RemovePlayFromChartAsync(
        string positionCode,
        int playerId, int teamId, int sportId);

    /// <summary>
    /// Retrieves the backups for a specific player and position.
    /// </summary>
    /// <param name="positionCode">The code representing the position.</param>
    /// <param name="playerId">The ID of the player.</param>
    /// <param name="teamId"></param>
    /// <param name="sportId"></param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result is a <see cref="PlayerDepthViewModel"/>
    /// object representing the backups for the player and position.</returns>
    public Task<List<PlayerViewModel>> GetBackUpsAsync(
        string positionCode, 
        int playerId,
        int teamId,
        int sportId);

    /// <summary>
    /// Gets the full depth chart for a team and sport.
    /// </summary>
    /// <param name="teamId">The ID of the team.</param>
    /// <param name="sportId"> The ID of the sport.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result is a <see cref="DepthChartViewModel"/>
    /// object representing the full depth chart for the team and sport.</returns>
    public Task<DepthChartViewModel> GetFullDepthChartAsync(
        int teamId,
        int sportId);
}