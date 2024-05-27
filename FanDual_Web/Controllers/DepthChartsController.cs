using FanDual_Web.Interfaces;
using FanDual_Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FanDual_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepthChartsController(ILogger<DepthChartsController> logger, IDataService dataService) : ControllerBase
    {
        /// <summary>
        /// Adds a player to the depth chart.
        /// </summary>
        /// <param name="addPlayerModel">The model containing the player information to add to the depth chart.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.
        /// The task result is an <see cref="IActionResult"/> indicating the status of the operation.</returns>
        [Route("/AddPlayerToDepthChart")]
        [HttpPost]
        public async Task<IActionResult> AddPlayerToDepthChart([FromBody] PlayerDepthRequestModel addPlayerModel)
        {
            if (addPlayerModel == null)
                throw new System.ArgumentNullException(nameof(addPlayerModel), "player is not defined");


            await dataService.AddPlayerToChartAsync(
                addPlayerModel.Position,
                addPlayerModel.PlayerId,
                addPlayerModel.PlayerDepth,
                addPlayerModel.TeamId,
                addPlayerModel.SportId);

            return Ok();
        }

        /// <summary>
        /// Removes a player from the depth chart.
        /// </summary>
        /// <param name="position">The position of the player.</param>
        /// <param name="playerId">The ID of the player to remove.</param>
        /// <param name="teamId">The ID of the team.</param>
        /// <param name="sportId">The ID of the sport.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result is an
        /// <see cref="IActionResult"/> indicating the status of the operation.</returns>
        [Route("/RemovePlayerFromDepthChart/{position}/{playerId}/{teamId}/{sportId}")]
        [HttpDelete]
        public async Task<IActionResult> RemovePlayerFromDepthChart(string position, int playerId, int teamId,
            int sportId)
        {
            if (string.IsNullOrEmpty(position))
                throw new System.ArgumentNullException(nameof(position), "position is null or empty");

            if (teamId == 0)
                throw new System.ArgumentNullException(nameof(teamId), "teamId is null or empty");

            if (sportId == 0)
                throw new System.ArgumentNullException(nameof(sportId), "sportId is null or empty");
            

            var deletedPlayer = await dataService.RemovePlayFromChartAsync(position, playerId, teamId, sportId);
            return Ok(deletedPlayer);
        }


        /// <summary>
        /// Retrieves the backups for a specific player and position.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="playerId">The ID of the player.</param>
        /// <param name="teamId">The ID of the team.</param>
        /// <param name="sportId">The ID of the sport.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The task result is a list of
        /// <see cref="PlayerViewModel"/> objects representing the backups for the player and position.</returns>
        [Route("/GetBackups/{position}/{playerId}/{teamId}/{sportId}")]
        [HttpGet]
        public async Task<IActionResult> GetBackups(string position, int playerId,int teamId,int sportId)
        {
            if (string.IsNullOrEmpty(position))
                throw new System.ArgumentNullException(nameof(position), "position is null or empty");
           
            if (teamId == 0)
                throw new System.ArgumentNullException(nameof(teamId), "teamId is null or empty");

            if (sportId == 0)
                throw new System.ArgumentNullException(nameof(sportId), "sportId is null or empty");
            
            
            var backupResult = await dataService.GetBackUpsAsync(position, playerId,teamId,sportId);
            return Ok(backupResult);
        }

        /// <summary>
        /// Gets the full depth chart for a team and sport.
        /// </summary>
        /// <param name="teamId">The ID of the team.</param>
        /// <param name="sportId"> The ID of the sport.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.
        /// The task result is a <see cref="DepthChartViewModel"/>
        /// object representing the full depth chart for the team and sport.</returns>
        [Route("/GetFullDepthChart/{teamId}/{sportId}")]
        [HttpGet]
        public async Task<IActionResult> GetFullDepthChart(int teamId,int sportId)
        {
            if (teamId == 0)
                throw new System.ArgumentNullException(nameof(teamId), "teamId is null or empty");

            if (sportId == 0)
                throw new System.ArgumentNullException(nameof(sportId), "sportId is null or empty");
            
            var depthChart = await dataService.GetFullDepthChartAsync(teamId,sportId);
           
            return Ok(depthChart);
        }
    }
}