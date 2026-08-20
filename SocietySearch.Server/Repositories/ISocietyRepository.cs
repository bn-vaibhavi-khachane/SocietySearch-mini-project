using SocietySearch.Server.Model.Domain;

namespace SocietySearch.Server.Repositories
{
    public interface ISocietyRepository
    {
        Task<List<Society>> GetSocietiesAsync(string? name = null, string? address = null);
        Task<Society?> GetSocietyByIdAsync(Guid id);
        Task<bool> SocietyExistsAsync(string name, string address, Guid? excludedSocietyId = null);
        Task<Society> CreateSocietyAsync(Society society);
        Task<Society> UpdateSocietyAsync(Society society);
        Task DeleteSocietyAsync(Society society);
    }
}
