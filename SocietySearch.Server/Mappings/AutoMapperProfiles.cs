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
            CreateMap<Society, SocietyDto>().ReverseMap();
            CreateMap<Society, AddSocietyRequestDto>().ReverseMap();
            CreateMap<Society, UpdateSocietyRequestDto>().ReverseMap();
            CreateMap<Amenities, AddAmenitiesDto>().ReverseMap();
            CreateMap<Units, UnitDto>().ReverseMap();
            CreateMap<Units, AddUnitRequestDto>().ReverseMap();
            CreateMap<Units, UpdateUnitRequestDto>().ReverseMap();
        }
    }
}
