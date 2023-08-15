using VNWalks.API.Models.Domain;
using VNWalks.API.Models.DTOs.Difficulty;
using VNWalks.API.Models.DTOs.Region;

namespace VNWalks.API.Models.DTOs.Walk
{
    public class WalkDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        public RegionDto Region { get; set; }

        public DifficultyDto Difficulty { get; set; }
    }
}
