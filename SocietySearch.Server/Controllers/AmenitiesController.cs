using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateAmenity([FromBody] AddAmenitiesDto amenityDto)
        {
            var amenityDomain = _mapper.Map<Model.Domain.Amenities>(amenityDto);
            amenityDomain.Id = Guid.NewGuid();

            await _amenitiesRepository.CreateAmenityAsync(amenityDomain);

            return Created($"api/Amenities/{amenityDomain.Id}", _mapper.Map<AmenitiesDto>(amenityDomain));
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteAmenity(Guid id)
        {
            var amenityDomain = await _amenitiesRepository.GetAmenityByIdAsync(id);
            if (amenityDomain == null)
            {
                return NotFound(new
                {
                    message = "Amenity not found."
                });
            }

            await _amenitiesRepository.DeleteAmenityAsync(amenityDomain);

            return NoContent();
        }
    }
}
