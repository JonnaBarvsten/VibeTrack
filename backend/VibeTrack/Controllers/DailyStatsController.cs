using Microsoft.AspNetCore.Mvc;
using VibeTrack.Application.DTOs.DailyStats;
using VibeTrack.Application.Interfaces;

namespace VibeTrack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyStatsController : ControllerBase
    {
        private readonly IDailyStatService _dailyStatService;
        public DailyStatsController(IDailyStatService dailyStatService)
        {
            _dailyStatService = dailyStatService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDailyStats([FromQuery] int userId) 
        {
            var dailyStats = await _dailyStatService.GetAllDailyStatsAsync(userId);

            return Ok(dailyStats);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetDailyStatById([FromRoute] int id, [FromQuery] int userId)
        {
            var dailyStat = await _dailyStatService.GetDailyStatByIdAsync(id, userId);

            if(dailyStat != null)
            {
                return Ok(dailyStat);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddDailyStat([FromBody] CreateDailyStatDto createDailyStatDto, [FromQuery] int userId )
        {
            var newDailyStat = await _dailyStatService.AddDailyStatAsync(createDailyStatDto, userId);

            return CreatedAtAction(nameof(GetDailyStatById), new { id = newDailyStat.Id }, newDailyStat);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateDailyStat([FromRoute] int id, [FromBody] UpdateDailyStatDto updateDailyStat, [FromQuery] int userId) 
        {
            var updatedDailyStat = await _dailyStatService.UpdateDailyStatAsync(id, updateDailyStat, userId);

            if(updatedDailyStat != null)
            {
                return Ok(updatedDailyStat);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteDailyStat([FromRoute] int id, [FromQuery] int userId)
        {
            var isDeleted = await _dailyStatService.DeleteDailyStatAsync(id, userId);

            if(isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
