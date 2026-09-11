using Microsoft.AspNetCore.Mvc;
using VibeTrack.Application.DTOs.DailyLogs;
using VibeTrack.Application.Interfaces;

namespace VibeTrack.Api.Controllers
{
    public class DailyLogsController : BaseApiController
    {
        private readonly IDailyLogService _dailyService;
        public DailyLogsController(IDailyLogService dailyService)
        {
            _dailyService = dailyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDailyLogs() 
        {
            var dailyLogs = await _dailyService.GetAllDailyLogsAsync(UserId);

            return Ok(dailyLogs);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetDailyLogById([FromRoute] int id)
        {
            var dailyLog = await _dailyService.GetDailyLogByIdAsync(id, UserId);

            if(dailyLog != null)
            {
               return Ok(dailyLog);
            }

            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> AddDailyLog([FromBody] CreateDailyLogDto createDailyLogDto)
        {
            var newDailyLog = await _dailyService.AddDailyLogAsync(createDailyLogDto, UserId);

            return CreatedAtAction(nameof(GetDailyLogById), new { id = newDailyLog.Id }, newDailyLog);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateDailyLog([FromRoute] int id, [FromBody] UpdateDailyLogDto updateDailyLogDto) 
        {
            var updatedDailyLog = await _dailyService.UpdateDailyLogAsync(id, updateDailyLogDto, UserId);

            if(updatedDailyLog != null)
            {
                return Ok(updatedDailyLog);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteDailyLog([FromRoute] int id)
        {
            var isDeleted = await _dailyService.DeleteDailyLogAsync(id, UserId);

            if(isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
