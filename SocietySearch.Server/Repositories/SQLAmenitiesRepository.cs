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
    }
}
