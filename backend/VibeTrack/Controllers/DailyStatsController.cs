using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VibeTrack.Application.DTOs.DailyStats;
using VibeTrack.Application.Interfaces;

namespace VibeTrack.Api.Controllers
{
    public class DailyStatsController : BaseApiController
    {
        private readonly IDailyStatService _dailyStatService;
        public DailyStatsController(IDailyStatService dailyStatService)
        {
            _dailyStatService = dailyStatService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDailyStats() 
        {
            var dailyStats = await _dailyStatService.GetAllDailyStatsAsync(UserId);

            return Ok(dailyStats);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetDailyStatById([FromRoute] int id)
        {
            var dailyStat = await _dailyStatService.GetDailyStatByIdAsync(id, UserId);

            if(dailyStat != null)
            {
                return Ok(dailyStat);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddDailyStat([FromBody] CreateDailyStatDto createDailyStatDto)
        {
            var newDailyStat = await _dailyStatService.AddDailyStatAsync(createDailyStatDto, UserId);

            return CreatedAtAction(nameof(GetDailyStatById), new { id = newDailyStat.Id }, newDailyStat);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateDailyStat([FromRoute] int id, [FromBody] UpdateDailyStatDto updateDailyStat) 
        {
            var updatedDailyStat = await _dailyStatService.UpdateDailyStatAsync(id, updateDailyStat, UserId);

            if(updatedDailyStat != null)
            {
                return Ok(updatedDailyStat);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteDailyStat([FromRoute] int id)
        {
            var isDeleted = await _dailyStatService.DeleteDailyStatAsync(id, UserId);

            if(isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
