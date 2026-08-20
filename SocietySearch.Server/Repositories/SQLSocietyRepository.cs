using Microsoft.EntityFrameworkCore;
using SocietySearch.Server.Data;
using SocietySearch.Server.Model.Domain;
using SocietySearch.Server.Repositories;

namespace NZWalks.API.Repositories
{
    public class SQLSocietyRepository : ISocietyRepository
    {
        private readonly SocietySearchDbContext _dbContext;
        public SQLSocietyRepository(SocietySearchDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<Society>> GetSocietiesAsync(string? name = null, string? address = null)
        {
            var query = _dbContext.Societies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(society => society.Name.Contains(name));
            }

            if (!string.IsNullOrWhiteSpace(address))
            {
                query = query.Where(society => society.Address.Contains(address));
            }

            return await query.ToListAsync();
        }

        public async Task<Society?> GetSocietyByIdAsync(Guid id)
        {
            return await _dbContext.Societies.FirstOrDefaultAsync(society => society.Id == id);
        }

        public async Task<bool> SocietyExistsAsync(
            string name,
            string address,
            Guid? excludedSocietyId = null)
        {
            return await _dbContext.Societies
                .AnyAsync(society =>
                    society.Name == name &&
                    society.Address == address &&
                    (!excludedSocietyId.HasValue || society.Id != excludedSocietyId.Value));
        }

        public async Task<Society> CreateSocietyAsync(Society society)
        {
            await _dbContext.Societies.AddAsync(society);
            await _dbContext.SaveChangesAsync();
            return society;
        }

        public async Task<Society> UpdateSocietyAsync(Society society)
        {
            await _dbContext.SaveChangesAsync();
            return society;
        }

        public async Task DeleteSocietyAsync(Society society)
        {
            _dbContext.Societies.Remove(society);
            await _dbContext.SaveChangesAsync();
        }

    }
}
