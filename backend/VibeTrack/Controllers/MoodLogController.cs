using Microsoft.AspNetCore.Mvc;
using VibeTrack.Application.DTOs.MoodLogDto;
using VibeTrack.Application.Interfaces;

namespace VibeTrack.Api.Controllers
{
    public class MoodLogController : BaseApiController
    {
        private readonly IMoodLogService _moodLogService;
        public MoodLogController(IMoodLogService moodLogService)
        {
            _moodLogService = moodLogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMoodLogs()
        {
            var moodLogs = await _moodLogService.GetAllMoodLogsAsync(UserId);

            return Ok(moodLogs);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMoodLogById([FromRoute] int id)
        {
            var moodLog = await _moodLogService.GetMoodLogByIdAsync(id, UserId);

            if(moodLog != null)
            {
                return Ok(moodLog);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddMoodLog([FromBody] CreateMoodLogDto createMoodLogDto)
        {
            var newMoodLog = await _moodLogService.AddMoodLogAsync(createMoodLogDto, UserId);

            if (newMoodLog == null)
            {
                return BadRequest("Obehörig eller ogiltig dagsstatistik.");
            }

            return CreatedAtAction(nameof(GetMoodLogById), new { id = newMoodLog.Id }, newMoodLog);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateMoodLog([FromRoute] int id, [FromBody] UpdateMoodLogDto updateMoodLogDto)
        {
            var updateMoodLog = await _moodLogService.UpdateMoodLogAsync(id, updateMoodLogDto, UserId);

            if(updateMoodLog != null)
            {
                return Ok(updateMoodLog);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteMoodLog([FromRoute] int id)
        {
            var isDeleted = await _moodLogService.DeleteMoodLogAsync(id, UserId);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
