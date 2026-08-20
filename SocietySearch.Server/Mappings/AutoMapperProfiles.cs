using AutoMapper;
using SocietySearch.Server.Model.Domain;
using SocietySearch.Server.Model.DTO;

namespace SocietySearch.Server.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Amenities, AmenitiesDto>().ReverseMap();
        }
    }
}
