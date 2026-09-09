using Microsoft.AspNetCore.Mvc;
using VibeTrack.Application.DTOs.MoodLogDto;
using VibeTrack.Application.Interfaces;

namespace VibeTrack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoodLogController : ControllerBase
    {
        private readonly IMoodLogService _moodLogService;
        public MoodLogController(IMoodLogService moodLogService)
        {
            _moodLogService = moodLogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMoodLogs([FromQuery] int userId)
        {
            var moodLogs = await _moodLogService.GetAllMoodLogsAsync(userId);

            return Ok(moodLogs);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMoodLogById([FromRoute] int id, [FromQuery] int userId)
        {
            var moodLog = await _moodLogService.GetMoodLogByIdAsync(id, userId);

            if(moodLog != null)
            {
                return Ok(moodLog);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddMoodLog([FromBody] CreateMoodLogDto createMoodLogDto, [FromQuery] int userId)
        {
            var newMoodLog = await _moodLogService.AddMoodLogAsync(createMoodLogDto, userId);

            if (newMoodLog == null)
            {
                return BadRequest("Obehörig eller ogiltig dagsstatistik.");
            }

            return CreatedAtAction(nameof(GetMoodLogById), new { id = newMoodLog.Id, userId = userId }, newMoodLog);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateMoodLog([FromRoute] int id, [FromBody] UpdateMoodLogDto updateMoodLogDto, [FromQuery] int userId)
        {
            var updateMoodLog = await _moodLogService.UpdateMoodLogAsync(id, updateMoodLogDto, userId);

            if(updateMoodLog != null)
            {
                return Ok(updateMoodLog);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> deleteMoodLog([FromRoute] int id, [FromQuery] int userId)
        {
            var isDeleted = await _moodLogService.DeleteMoodLogAsync(id, userId);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
