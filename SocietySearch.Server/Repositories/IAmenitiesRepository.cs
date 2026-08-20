using SocietySearch.Server.Model.Domain;

namespace SocietySearch.Server.Repositories
{
    public interface IAmenitiesRepository
    {
        Task<List<Amenities>> GetAllAmenitiesAsync();
        Task<List<Guid>> GetMissingAmenityIdsAsync(IEnumerable<Guid> amenityIds);
    }
}
