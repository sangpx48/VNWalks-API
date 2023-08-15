using VNWalks.API.Models.Domain;

namespace VNWalks.API.Repositories
{
    public class SQLDifficultRepository : IDifficultyRepository
    {
        public Task<Difficulty> CreateAsync(Difficulty difficulty)
        {
            throw new NotImplementedException();
        }
    }
}
