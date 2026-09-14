using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VibeTrack.Application.DTOs.Activities;
using VibeTrack.Application.Interfaces;

namespace VibeTrack.Api.Controllers
{
    public class ActivityController : BaseApiController
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActivities()
        {
            var activities = await _activityService.GetAllActivitiesAsync(UserId);

            return Ok(activities);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetActivityById([FromRoute] int id)
        {
            var activity = await _activityService.GetActivityByIdAsync(id, UserId);

            if (activity != null)
            {
                return Ok(activity);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddActivity([FromBody] CreateActivityDto createActivityDto)
        {
            var newActivity = await _activityService.AddActivityAsync(createActivityDto, UserId);

            if (newActivity == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetActivityById),new { id = newActivity.Id },newActivity);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateActivity([FromRoute] int id, [FromBody] UpdateActivityDto updateActivityDto)
        {
            var updatedActivity = await _activityService.UpdateActivityAsync(id, updateActivityDto, UserId);

            if (updatedActivity != null)
            {
                return Ok(updatedActivity);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteActivity([FromRoute] int id)
        {
            var isDeleted = await _activityService.DeleteActivityAsync(id, UserId);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
