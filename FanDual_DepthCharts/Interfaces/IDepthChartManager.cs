namespace FanDual_DepthCharts.Interfaces;

public interface IDepthChartManager
{
    public Task<ResponseAddPlayerToDepthChart> AddPlayerToChart(RequestAddPlayerToDepthChart addPlayer);
    public Task<ResponseGetBackUps> GetBackups(RequestGetBackUps request);
    public Task<ResponseRemovePlayerFromDepthChart> RemovePlayerFromChart(RequestRemovePlayerFromDepthChart request);
    public Task<ResponseGetFullDepthChart> GetFullDepthChart(RequestGetFullDepthChart request);
}