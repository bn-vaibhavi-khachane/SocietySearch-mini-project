using Microsoft.EntityFrameworkCore;
using SocietySearch.Server.Data;
using SocietySearch.Server.Model.Domain;

namespace SocietySearch.Server.Repositories
{
    public class SQLUnitsRepository : IUnitsRepository
    {
        private readonly SocietySearchDbContext _dbContext;

        public SQLUnitsRepository(SocietySearchDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Units>> GetUnitsAsync(Guid? societyId = null)
        {
            var query = _dbContext.Units.AsQueryable();

            if (societyId.HasValue)
            {
                query = query.Where(unit => unit.SocietyId == societyId.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<Units?> GetUnitByIdAsync(Guid id)
        {
            return await _dbContext.Units.FirstOrDefaultAsync(unit => unit.Id == id);
        }

        public async Task<Units> CreateUnitAsync(Units unit)
        {
            await _dbContext.Units.AddAsync(unit);
            await _dbContext.SaveChangesAsync();
            return unit;
        }

        public async Task<Units> UpdateUnitAsync(Units unit)
        {
            await _dbContext.SaveChangesAsync();
            return unit;
        }

        public async Task DeleteUnitAsync(Units unit)
        {
            _dbContext.Units.Remove(unit);
            await _dbContext.SaveChangesAsync();
        }
    }
}