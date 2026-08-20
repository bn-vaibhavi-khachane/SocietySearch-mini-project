using SocietySearch.Server.Model.Domain;

namespace SocietySearch.Server.Repositories
{
    public interface IAmenitiesRepository
    {
        Task<List<Amenities>> GetAllAmenitiesAsync();
        Task<Amenities?> GetAmenityByIdAsync(Guid id);
        Task<List<Guid>> GetMissingAmenityIdsAsync(IEnumerable<Guid> amenityIds);
        Task<Amenities> CreateAmenityAsync(Amenities amenity);
        Task DeleteAmenityAsync(Amenities amenity);
    }
}
