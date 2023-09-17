using VNWalks.API.Models.Domain;
using VNWalks.API.Repositories.Interface;

namespace VNWalks.API.Repositories.Implement
{
    public class DifficultRepository : IDifficultyRepository
    {
        public Task<Difficulty> CreateAsync(Difficulty difficulty)
        {
            throw new NotImplementedException();
        }
    }
}
