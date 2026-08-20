using Microsoft.EntityFrameworkCore;
using SocietySearch.Server.Data;
using SocietySearch.Server.Model.Domain;

namespace SocietySearch.Server.Repositories
{
    public class SQLAmenitiesRepository : IAmenitiesRepository
    {
        private readonly SocietySearchDbContext _dbContext;
        public SQLAmenitiesRepository(SocietySearchDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<List<Amenities>> GetAllAmenitiesAsync()
        {
            return await _dbContext.Amenities.ToListAsync();
        }

        public async Task<Amenities?> GetAmenityByIdAsync(Guid id)
        {
            return await _dbContext.Amenities.FirstOrDefaultAsync(amenity => amenity.Id == id);
        }

        public async Task<List<Guid>> GetMissingAmenityIdsAsync(IEnumerable<Guid> amenityIds)
        {
            var requestedIds = amenityIds.Distinct().ToList();
            var existingIds = await _dbContext.Amenities
                .Where(amenity => requestedIds.Contains(amenity.Id))
                .Select(amenity => amenity.Id)
                .ToListAsync();

            return requestedIds.Except(existingIds).ToList();
        }

        public async Task<Amenities> CreateAmenityAsync(Amenities amenity)
        {
            await _dbContext.Amenities.AddAsync(amenity);
            await _dbContext.SaveChangesAsync();
            return amenity;
        }

        public async Task DeleteAmenityAsync(Amenities amenity)
        {
            _dbContext.Amenities.Remove(amenity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
