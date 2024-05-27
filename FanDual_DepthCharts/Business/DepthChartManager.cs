using FanDual_Data.Interfaces;
using FanDual_Data.Models;
using FanDual_DepthCharts.Interfaces;
using FanDual_DepthCharts.Protos;
using Google.Protobuf.Collections;

namespace FanDual_DepthCharts.Business;

public class DepthChartManager(ILogger<DepthChartManager> logger, IRepository repository) : IDepthChartManager
{
    /// <summary>
    /// Adds a player to the depth chart.
    /// </summary>
    /// <param name="addPlayer">The request containing the player's information.</param>
    /// <returns>The response containing the success status of adding the player to the depth chart.</returns>
    public async Task<ResponseAddPlayerToDepthChart> AddPlayerToChart(
        RequestAddPlayerToDepthChart addPlayer)
    {
        var error = addPlayer.ValidateRequest();
        if(error!=null)
        {
            throw error;
        }

        var result = await repository.AddPlayerToChart(
            addPlayer.PositionCode,
            (int)addPlayer.PlayerId,
            (int)addPlayer.PlayerDepth,
            (int)addPlayer.TeamId,
            (int)addPlayer.SportId);

        return new ResponseAddPlayerToDepthChart { Success = result };
    }

    public async Task<ResponseGetBackUps> GetBackups(
        RequestGetBackUps request)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Removes a player from the depth chart.
    /// </summary>
    /// <param name="request">The request containing the player's information.</param>
    /// <returns>The response containing the updated player depth.</returns>
    public async Task<ResponseRemovePlayerFromDepthChart> RemovePlayerFromChart(
        RequestRemovePlayerFromDepthChart request)
    {
        var error = request.ValidateRequest();
        if(error!=null)
        {
            throw error;
        }

        var result = await repository.RemovePlayFromChart(
            request.PositionCode,
            (int)request.PlayerId,
            (int)request.TeamId,
            (int)request.SportId);


        var playerDepth = new SportPlayerDepth
        {
            SportId = request.SportId,
            TeamId = request.TeamId,
            PositionCode = request.PositionCode,
            PlayerId = result.PlayerId,
            Id = result.Id,
            PositionDepth = result.PositionDepth,
            PositionId = result.PositionId,
            SportName = result.DepthCharts.Sport.SportName,
            PlayerName = result.Player.Name,
            TeamName = result.DepthCharts.Team.Name,
            HeadPositionId = result.Position.HeadPosition.Id,
            HeadPositionCode = result.Position.HeadPosition.HeadCode
        };

        return new ResponseRemovePlayerFromDepthChart
        {
            PlayerDepth = playerDepth
        };
    }

    /// <summary>
    /// Retrieves the full depth chart for a given team and sport.
    /// </summary>
    /// <param name="request">The request containing the team ID and sport ID.</param>
    /// <returns>The response containing the full depth chart.</returns>
    public async Task<ResponseGetFullDepthChart> GetFullDepthChart(RequestGetFullDepthChart request)
    {
        var error = request.ValidateRequest();
        if(error!=null)
        {
            throw error;
        }

        var result = await repository.GetFullDepthChart((int)request.TeamId, (int)request.SportId);

        var listHeadPosition =  new List<PositionHeader>();

        if (result.SportsPlayersDepths.Count > 0)
        {
            var headCodeGroup= result.SportsPlayersDepths
                .GroupBy(s => s.Position.HeadPosition.HeadCode);
            
            
            
            foreach (var chartPlayer in headCodeGroup)
            {
                var key = chartPlayer.Key;
                
                var dataDic = new Dictionary<string,Position >();
                foreach (var dataPosition in chartPlayer.GroupBy(d=>d.Position.Code))
                {
                    var posCode = dataPosition.Key;
                    var positionPlayerList = dataPosition.Select(pos => new PlayerDepth
                        {
                            Name = pos.Player.Name, 
                            Id = pos.PlayerId, 
                            Number = pos.Player.Number, 
                            Depth = pos.PositionDepth,
                        }).ToList();

                    var position = new Position{Players = { positionPlayerList }};
                    dataDic.Add(posCode,position);
                }
                
                listHeadPosition.Add(new PositionHeader
                {
                    HeaderCode = key,
                    DepthPositions = {dataDic}
                });
            }
        }
        
        var response = new ResponseGetFullDepthChart
        {
            DepthChart = new FullDepthChart
            {
                Sport = result.Sport.SportName,
                Team = result.Team.Name,
                PositionDepth ={listHeadPosition}
            }
        };

        return response;
    }
}