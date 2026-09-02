using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VibeTrack.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } 

        [Required]
        [MaxLength (100)]
        public string LastName { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<DailyLog> DailyLogs { get; set; }
    }
}
