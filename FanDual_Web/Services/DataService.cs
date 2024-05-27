using FanDual_DepthCharts;
using FanDual_Web.Interfaces;
using FanDual_Web.Models;

namespace FanDual_Web.Services;

public class DataService(
    ILogger<DataService> logger,
    DepthChart.DepthChartClient depthChartClient) : IDataService
{
    private readonly DepthChart.DepthChartClient _depthChartClient =
        depthChartClient ?? throw new ArgumentNullException(nameof(depthChartClient));


    /// <summary>
    /// Adds a player to the depth chart.
    /// </summary>
    /// <param name="positionCode">The position code of the player.</param>
    /// <param name="playerId">The ID of the player.</param>
    /// <param name="positionDepth">The depth of the player's position in the chart.</param>
    /// <param name="teamId">The ID of the team.</param>
    /// <param name="sportId">The ID of the sport.</param>
    /// <returns>Returns a task representing the asynchronous operation.
    /// The task will complete with the result indicating if the player was successfully added to the chart.</returns>
    public async Task<bool> AddPlayerToChart(string positionCode, int playerId, int positionDepth, int teamId,
        int sportId)
    {
        var result = await _depthChartClient.AddPlayerToChartAsync(new RequestAddPlayerToDepthChart
        {
            PlayerDepth = positionDepth,
            PositionCode = positionCode,
            PlayerId = playerId,
            TeamId = teamId,
            SportId = sportId
        });

        return result.Success;
    }

    /// <summary>
    /// Removes a play from the depth chart.
    /// </summary>
    /// <param name="positionCode">The position code of the player.</param>
    /// <param name="playerId">The ID of the player.</param>
    /// <param name="teamId">The ID of the team.</param>
    /// <param name="sportId">The ID of the sport.</param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// The task will complete with the result indicating if the play was successfully removed from the chart.
    /// </returns>
    public async Task<PlayerDepthViewModel> RemovePlayFromChart(string positionCode, int playerId, int teamId,
        int sportId)
    {
        var result = await _depthChartClient.RemovePlayerFromChartAsync(new RequestRemovePlayerFromDepthChart
        {
            PlayerId = playerId,
            PositionCode = positionCode,
            SportId = sportId,
            TeamId = teamId,
        });


        if (result.PlayerDepth == null)
        {
            var chart = new PlayerDepthViewModel
            {
                Id = 0,
                PositionId = 0,
                PositionCode = "",
                HeadPositionCode = "",
                HeadPositionId = 0,
                PlayerId = 0,
                PlayerName = "",
                PositionDepth = 0,
                SportId = 0,
                SportName = "",
                TeamId = 0,
                TeamNane = ""
            };
            return chart;
        }

        var depth = result.PlayerDepth;
        return new PlayerDepthViewModel
        {
            Id = (int)depth.Id,
            PositionId = (int)depth.PositionId,
            PositionCode = depth.PositionCode,
            HeadPositionCode = depth.HeadPositionCode,
            HeadPositionId = (int)depth.HeadPositionId,
            PlayerId = (int)depth.PlayerId,
            PlayerName = depth.PlayerName,
            PositionDepth = (int)depth.PositionDepth,
            SportId = (int)depth.SportId,
            SportName = depth.SportName,
            TeamId = (int)depth.TeamId,
            TeamNane = depth.TeamName
        };
    }

    /// <summary>
    /// Retrieves a list of backup players for the specified position and team.
    /// </summary>
    /// <param name="positionCode">The position code of the main player.</param>
    /// <param name="playerId">The ID of the main player.</param>
    /// <param name="teamId">The ID of the team.</param>
    /// <param name="sportId">The ID of the sport.</param>
    /// <returns>Returns a task representing the asynchronous operation.
    /// The task will complete with a List of PlayerViewModel objects representing the backup players.</returns>
    public async Task<List<PlayerViewModel>> GetBackUps(
        string positionCode,
        int playerId, int teamId,
        int sportId)
    {
        var result = await _depthChartClient.GetBackupsAsync(new RequestGetBackUps
        {
            PlayerId = playerId,
            PositionCode = positionCode,
            SportId = sportId,
            TeamId = teamId
        });


        var responseList = result.Players.Select(s => new PlayerViewModel
        {
            Id = (int)s.Id,
            Name = s.Name,
            Number = (int)s.Number
        }).ToList();

        return responseList;
    }

    /// <summary>
    /// Retrieves the full depth chart for a team and sport.
    /// </summary>
    /// <param name="teamId">The ID of the team.</param>
    /// <param name="sportId">The ID of the sport.</param>
    /// <returns>Returns a task representing the asynchronous operation.
    /// The task will complete with the DepthChartViewModel representing
    /// the full depth chart for the specified team and sport.</returns>
    public async Task<DepthChartViewModel> GetFullDepthChart(int teamId, int sportId)
    {
        var result = await _depthChartClient.GetFullDepthChartAsync(new RequestGetFullDepthChart
        {
            SportId = sportId,
            TeamId = teamId
        });

        if (result?.DepthChart == null)
        {
            return new DepthChartViewModel
            {
                Sport = "",
                Team = "",
                PositionHeaders = []
            };
        }

        var depth = result.DepthChart;
        var responseChart = new DepthChartViewModel
        {
            Sport = depth.Sport,
            Team = depth.Team,
            PositionHeaders = []
        };

        foreach (var positionHeader in depth.PositionDepth)
        {
            var header = new HeaderViewModel
                { Code = positionHeader.HeaderCode, DepthPositions = new Dictionary<string, List<DepthViewModel>>() };

            var dic = positionHeader.DepthPositions.Select(s =>
            {
                var key = s.Key;
                var list = s.Value.Players.Select(x => new DepthViewModel
                {
                    Id = (int)x.Id,
                    Name = x.Name,
                    Number = (int)x.Number,
                    Depth = (int)x.Depth
                }).ToList();

                return new { key, list };
            }).ToDictionary(dp => dp.key, dp => dp.list);

            header.DepthPositions = dic;
            responseChart.PositionHeaders.Add(header);
        }

        return responseChart;
    }
}