using FanDual_Data.Interfaces;
using FanDual_Data.Models;
using Microsoft.EntityFrameworkCore;


namespace FanDual_Data.Repository;

public class DepthChartRepository(FanDualContext fanDualContext) : IRepository
{
    /// <summary>
    /// Adds a player to the depth chart at a specific position and depth.
    /// </summary>
    /// <param name="positionCode">The code of the position to add the player to.</param>
    /// <param name="playerId">The ID of the player to add.</param>
    /// <param name="positionDepth">The depth at which the player should be added.</param>
    /// <param name="teamId"></param>
    /// <param name="sportId"></param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task<bool> AddPlayerToChart(
        string positionCode,
        int playerId, int positionDepth, int teamId, int sportId)
    {
        
        var queueDepthList = await fanDualContext.DepthCharts
            .Include(s => s.SportsPlayersDepths)
            .Include(sp => sp.Sport)
            .Include(s => s.Team)
            .Include(s => s.SportsPlayersDepths)
            .ThenInclude(s => s.Position)
            .Include(s => s.SportsPlayersDepths)
            .ThenInclude(s => s.Player)
            .FirstOrDefaultAsync(s => s.TeamId == teamId && s.SportId == sportId);

        var newDepthQueue = new Models.SportsPlayersDepth
        {
            PositionDepth = positionDepth,
            Position = fanDualContext.Positions.First(s => s.Code == positionCode),
            Player = fanDualContext.Players.First(s => s.Id == playerId)
        };


        if (queueDepthList == null) return false;

        var playerDepth = queueDepthList.SportsPlayersDepths
            .Where(s => s.Position.Code == positionCode)
            .OrderBy(s => s.PositionDepth);

        foreach (var players in playerDepth)
        {
            if (positionDepth > players.PositionDepth)
            {
                continue;
            }

            players.PositionDepth = positionDepth + 1;
        }

        queueDepthList.SportsPlayersDepths.Add(newDepthQueue);
        _ = await fanDualContext.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Removes a player from the depth chart at a specified position and player ID.
    /// </summary>
    /// <param name="positionCode">The code of the position from which to remove the player.</param>
    /// <param name="playerId">The ID of the player to remove from the depth chart.</param>
    /// <param name="teamId"></param>
    /// <param name="sportId"></param>
    /// <returns>A task representing the asynchronous operation. The removed SportsPlayersDepth object if found, otherwise a new SportsPlayersDepth object.</returns>
    public async Task<Models.SportsPlayersDepth> RemovePlayFromChart(
        string positionCode,
        int playerId, int teamId, int sportId)
    {
        var depth = await fanDualContext.SportsPlayersDepths
            .Include(s => s.Player)
            .Include(a => a.Position)
            .ThenInclude(s => s.HeadPosition)
            .Include(a => a.DepthCharts)
            .Include(a => a.DepthCharts.Sport)
            .Include(a => a.DepthCharts.Team)
            .FirstOrDefaultAsync(s => s.PlayerId == playerId
                                      && s.Position.Code == positionCode
                                      && s.DepthCharts.TeamId == teamId
                                      && s.DepthCharts.SportId == sportId);

        if (depth != null)
        {
            fanDualContext.SportsPlayersDepths.Remove(depth);

            return depth;
        }

        return new SportsPlayersDepth();
    }

    /// <summary>
    /// Retrieves a list of backup players for a given position and player ID.
    /// </summary>
    /// <param name="positionCode">The code of the position for which to retrieve the backup players.</param>
    /// <param name="playerId">The ID of the player for which to retrieve the backup players.</param>
    /// <returns>A task representing the asynchronous operation that returns a list of
    /// <see cref="Player"/> objects representing the backup players.</returns>
    public async Task<List<Models.Player>> GetBackUps(
        string positionCode,
        int playerId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Retrieves the full depth chart for a sport team.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous operation that returns an instance of DepthChart,
    /// which represents the full depth chart for the sport team.
    /// </returns>
    public async Task<Models.DepthChart> GetFullDepthChart(int teamId, int sportId)
    {
        return await GetSportTeamDepthChart(teamId,sportId);
    }

    /// <summary>
    /// Retrieves the depth chart for a specific sports team.
    /// </summary>
    /// <param name="teamId">The ID of the team.</param>
    /// <param name="sportId">The ID of the sport.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the depth chart.</returns>
    private async Task<Models.DepthChart> GetSportTeamDepthChart(int teamId, int sportId)
    {
        var data = await fanDualContext.DepthCharts
            .Include(dc => dc.Team)
            .Include(dc => dc.Sport)
            .Include(dc => dc.SportsPlayersDepths)
            .ThenInclude(dc => dc.Position)
            .ThenInclude(s=>s.HeadPosition)
            .Include(dc => dc.SportsPlayersDepths)
            .ThenInclude(dc => dc.Player)
            .FirstAsync(w => w.TeamId == teamId && w.SportId == sportId);

        return data;
    }
}