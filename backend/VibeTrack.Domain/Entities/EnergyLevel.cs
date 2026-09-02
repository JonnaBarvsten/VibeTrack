using System.ComponentModel.DataAnnotations;

namespace VibeTrack.Domain.Entities
{
    public class EnergyLevel
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(0,5)]
        public int Level { get; set; }
    }
}
