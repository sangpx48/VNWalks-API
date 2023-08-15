using VNWalks.API.Models.Domain;

namespace VNWalks.API.Repositories
{
    public interface IDifficultyRepository
    {
        Task<Difficulty> CreateAsync(Difficulty difficulty);
    }
}
