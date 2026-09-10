using Microsoft.AspNetCore.Mvc;
using VibeTrack.Application.DTOs.Activities;
using VibeTrack.Application.Interfaces;

namespace VibeTrack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActivities([FromQuery] int userId)
        {
            var activities = await _activityService.GetAllActivitiesAsync(userId);

            return Ok(activities);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetActivityById([FromRoute] int id, [FromQuery] int userId)
        {
            var activity = await _activityService.GetActivityByIdAsync(id, userId);

            if (activity != null)
            {
                return Ok(activity);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddActivity([FromBody] CreateActivityDto createActivityDto, [FromQuery] int userId)
        {
            var newActivity = await _activityService.AddActivityAsync(createActivityDto, userId);

            if (newActivity == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetActivityById),new { id = newActivity.Id, userId = userId },newActivity);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateActivity([FromRoute] int id, [FromBody] UpdateActivityDto updateActivityDto, [FromQuery] int userId)
        {
            var updatedActivity = await _activityService.UpdateActivityAsync(id, updateActivityDto, userId);

            if (updatedActivity != null)
            {
                return Ok(updatedActivity);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteActivity([FromRoute] int id, [FromQuery] int userId)
        {
            var isDeleted = await _activityService.DeleteActivityAsync(id, userId);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
