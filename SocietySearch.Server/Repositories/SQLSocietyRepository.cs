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

        public async Task<List<Society>> GetSocietiesAsync()
        {
            return await _dbContext.Societies.ToListAsync();
        }

    }
}
