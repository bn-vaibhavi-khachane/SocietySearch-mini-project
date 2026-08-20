using SocietySearch.Server.Model.Domain;

namespace SocietySearch.Server.Repositories
{
    public interface IAmenitiesRepository
    {
        Task<List<Amenities>> GetAllAmenitiesAsync();
    }
}
