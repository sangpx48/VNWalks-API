using VNWalks.API.Models.Domain;

namespace VNWalks.API.Repositories
{
    public interface IRegionRepositoty
    {
        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        Task<List<Region>> GetAllAsync();

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Region?> GetByIdAsync(Guid id);

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        Task<Region> CreateAsync(Region region);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        Task<Region?> UpdateAsync(Guid id, Region region);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Region?> DeleteAsync(Guid id);
    }
}
