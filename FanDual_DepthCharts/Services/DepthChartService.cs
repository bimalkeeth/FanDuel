using FanDual_DepthCharts.Interfaces;
using Grpc.Core;

namespace FanDual_DepthCharts.Services;

public class DepthChartService(ILogger<DepthChartService> logger,IDepthChartManager depthChartManager) : DepthChart.DepthChartBase
{
    /// <summary>
    /// Retrieves backups for a depth chart.
    /// </summary>
    /// <param name="request">The request for backups</param>
    /// <param name="context">The server call context</param>
    /// <returns>The response containing the backups</returns>
    public override Task<ResponseGetBackUps> GetBackups(
        RequestGetBackUps request, ServerCallContext context)
    {
        return depthChartManager.GetBackups(request);
    }

    /// <summary>
    /// Adds a player to the depth chart.
    /// </summary>
    /// <param name="request">The request to add a player to the depth chart</param>
    /// <param name="context">The server call context</param>
    /// <returns>The response indicating the success or failure of adding the player to the depth chart</returns>
    public override Task<ResponseAddPlayerToDepthChart> AddPlayerToChart(
        RequestAddPlayerToDepthChart request, ServerCallContext context)
    {
        return depthChartManager.AddPlayerToChart(request);
    }

    /// <summary>
    /// Removes a player from the depth chart.
    /// </summary>
    /// <param name="request">The request to remove a player from the depth chart</param>
    /// <param name="context">The server call context</param>
    /// <returns>The response indicating the status of the player removal</returns>
    public override Task<ResponseRemovePlayerFromDepthChart> RemovePlayerFromChart(
        RequestRemovePlayerFromDepthChart request, ServerCallContext context)
    {
        return depthChartManager.RemovePlayerFromChart(request);
    }

    /// <summary>
    /// Retrieves the full depth chart.
    /// </summary>
    /// <param name="request">The request for the full depth chart</param>
    /// <param name="context">The server call context</param>
    /// <returns>The response containing the full depth chart</returns>
    public override Task<ResponseGetFullDepthChart> GetFullDepthChart(
        RequestGetFullDepthChart request, ServerCallContext context)
    {
        return depthChartManager.GetFullDepthChart(request);
    }
}