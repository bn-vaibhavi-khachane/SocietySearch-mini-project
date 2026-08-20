using SocietySearch.Server.Model.Domain;

namespace SocietySearch.Server.Repositories
{
    public interface ISocietyRepository
    {
        Task<List<Society>> GetSocietiesAsync(); 
    }
}
