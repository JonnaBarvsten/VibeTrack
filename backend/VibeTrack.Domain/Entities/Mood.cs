using System.ComponentModel.DataAnnotations;

namespace VibeTrack.Domain.Entities
{
    public class Mood
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
