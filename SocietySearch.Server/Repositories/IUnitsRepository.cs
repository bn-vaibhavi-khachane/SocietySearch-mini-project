using SocietySearch.Server.Model.Domain;

namespace SocietySearch.Server.Repositories
{
    public interface IUnitsRepository
    {
        Task<List<Units>> GetUnitsAsync(Guid? societyId = null);
        Task<Units?> GetUnitByIdAsync(Guid id);
        Task<Units> CreateUnitAsync(Units unit);
        Task<Units> UpdateUnitAsync(Units unit);
        Task DeleteUnitAsync(Units unit);
    }
}