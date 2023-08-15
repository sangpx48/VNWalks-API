
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using VNWalks.API.Data;
using VNWalks.API.Models.Domain;

namespace VNWalks.API.Repositories
{
    /// <summary>
    /// trien khai DbContext de truyen vao CSDL
    /// </summary>
    public class SQLRegionRepository : IRegionRepositoty
    {
        private readonly VNWalksDbContext dbContext;

        public SQLRegionRepository(VNWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            // Save Modifies to Database
            await dbContext.SaveChangesAsync();
            return region;
        }

        /// <summary>
        ///  Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            //Check region existed 
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            //Update region vs existingRegion
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            // Save Modifies to Database
            await dbContext.SaveChangesAsync();

            return existingRegion;
        }

        /// <summary>
        ///  Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Region?> DeleteAsync(Guid id)
        {
            //Check region existed 
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            dbContext.Regions.Remove(existingRegion);
            // Save Modifies to Database
            await dbContext.SaveChangesAsync();

            //tra lai doi tuong da Xoa
            return existingRegion;
        }
    }
}
