using Microsoft.AspNetCore.Mvc;
using VibeTrack.Application.DTOs.DailyLogs;
using VibeTrack.Application.Interfaces;

namespace VibeTrack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyLogsController : ControllerBase
    {
        private readonly IDailyLogService _dailyService;
        public DailyLogsController(IDailyLogService dailyService)
        {
            _dailyService = dailyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDailyLogs([FromQuery] int userId) 
        {
            var dailyLogs = await _dailyService.GetAllDailyLogsAsync(userId);

            return Ok(dailyLogs);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetDailyLogById([FromRoute] int id, [FromQuery] int userId)
        {
            var dailyLog = await _dailyService.GetDailyLogByIdAsync(id, userId);

            if(dailyLog != null)
            {
               return Ok(dailyLog);
            }

            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> AddDailyLog([FromBody] CreateDailyLogDto createDailyLogDto, [FromQuery] int userId)
        {
            var newDailyLog = await _dailyService.AddDailyLogAsync(createDailyLogDto, userId);

            return CreatedAtAction(nameof(GetDailyLogById), new { id = newDailyLog.Id, userId = userId }, newDailyLog);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateDailyLog([FromRoute] int id, [FromBody] UpdateDailyLogDto updateDailyLogDto, [FromQuery] int userId) 
        {
            var updatedDailyLog = await _dailyService.UpdateDailyLogAsync(id, updateDailyLogDto, userId);

            if(updatedDailyLog != null)
            {
                return Ok(updatedDailyLog);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteDailyLog([FromRoute] int id, [FromQuery] int userId)
        {
            var isDeleted = await _dailyService.DeleteDailyLogAsync(id, userId);

            if(isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
