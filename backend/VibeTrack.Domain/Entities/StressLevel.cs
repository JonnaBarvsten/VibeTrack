using System.ComponentModel.DataAnnotations;

namespace VibeTrack.Domain.Entities
{
    public class StressLevel
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
       
        [Required]
        [Range(0,10)]
        public int Level { get; set; }
    }
}
