namespace FanDual_DepthCharts.Interfaces;

public interface IDepthChartManager
{
    public Task<ResponseAddPlayerToDepthChart> AddPlayerToChartAsync(RequestAddPlayerToDepthChart addPlayer);
    public Task<ResponseGetBackUps> GetBackupsAsync(RequestGetBackUps request);
    public Task<ResponseRemovePlayerFromDepthChart> RemovePlayerFromChartAsync(RequestRemovePlayerFromDepthChart request);
    public Task<ResponseGetFullDepthChart> GetFullDepthChartAsync(RequestGetFullDepthChart request);
}