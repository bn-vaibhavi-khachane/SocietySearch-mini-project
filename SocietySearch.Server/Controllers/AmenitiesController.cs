using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocietySearch.Server.Model.DTO;
using SocietySearch.Server.Repositories;

namespace SocietySearch.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenitiesRepository _amenitiesRepository;
        private readonly IMapper _mapper;
        public AmenitiesController(IMapper mapper, IAmenitiesRepository amenitiesRepository)
        {
            this._mapper = mapper;
            this._amenitiesRepository = amenitiesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAmenities() { 
            var amenityDomain = await _amenitiesRepository.GetAllAmenitiesAsync();
            return Ok(_mapper.Map<List<AmenitiesDto>>(amenityDomain));
        }
    }
}
