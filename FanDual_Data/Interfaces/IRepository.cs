namespace FanDual_Data.Interfaces;

public interface IRepository
{
    /// <summary>
    /// Adds a player to the depth chart at a specified position and depth.
    /// </summary>
    /// <param name="positionCode">The code of the position to add the player to.</param>
    /// <param name="playerId">The ID of the player to add to the depth chart.</param>
    /// <param name="positionDepth">The depth at which the player should be added.</param>
    /// <param name="teamId"></param>
    /// <param name="sportId"></param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task<bool> AddPlayerToChart(string positionCode, int playerId, int positionDepth,int teamId, int sportId);

    /// <summary>
    /// Removes a player from the depth chart at a specified position and player ID.
    /// </summary>
    /// <param name="positionCode">The code of the position from which to remove the player.</param>
    /// <param name="playerId">The ID of the player to remove from the depth chart.</param>
    /// <param name="teamId"></param>
    /// <param name="sportId"></param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task<Models.SportsPlayersDepth> RemovePlayFromChart(string positionCode, int playerId,int teamId, int sportId);

    /// <summary>
    /// Retrieves a list of backup players for a given position and player ID.
    /// </summary>
    /// <param name="positionCode">The code of the position for which to retrieve the backup players.</param>
    /// <param name="playerId">The ID of the player for which to retrieve the backup players.</param>
    /// <returns>A task representing the asynchronous operation that returns a list of
    /// <see cref="Player"/> objects representing the backup players.</returns>
    public Task<List<Models.Player>> GetBackUps(string positionCode, int playerId);

    /// <summary>
    /// Retrieves the full depth chart for a sport team.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous operation that returns an instance of DepthChart,
    /// which represents the full depth chart for the sport team.
    /// </returns>
    public Task<Models.DepthChart> GetFullDepthChart(int teamId, int sportId);
}