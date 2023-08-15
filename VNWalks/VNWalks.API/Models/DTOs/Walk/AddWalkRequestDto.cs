using System.ComponentModel.DataAnnotations;
using VNWalks.API.Models.Domain;

namespace VNWalks.API.Models.DTOs.Walk
{
    public class AddWalkRequestDto
    {

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [Range(0, 500)]
        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        [Required]

        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }


    }
}
