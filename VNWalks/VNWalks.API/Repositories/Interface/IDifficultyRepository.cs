using VNWalks.API.Models.Domain;

namespace VNWalks.API.Repositories.Interface
{
    public interface IDifficultyRepository
    {
        Task<Difficulty> CreateAsync(Difficulty difficulty);
    }
}
