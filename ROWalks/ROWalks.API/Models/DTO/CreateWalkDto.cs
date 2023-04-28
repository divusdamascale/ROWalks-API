using System.ComponentModel.DataAnnotations;

namespace ROWalks.API.Models.DTO
{
    public class CreateWalkDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        [Required]
        [Range(1,50)]
        public double LengthInKm { get; set; }
        public string? WalkImageURL { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid CountyId { get; set; }

    }
}
