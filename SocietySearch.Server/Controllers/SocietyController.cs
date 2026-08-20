using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using SocietySearch.Server.Model.DTO;
using SocietySearch.Server.Repositories;
using System.Security.Claims;

namespace SocietySearch.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocietyController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISocietyRepository _societyRepository;
        private readonly IAmenitiesRepository _amenitiesRepository;

        public SocietyController(
            IMapper mapper,
            ISocietyRepository societyRepository,
            IAmenitiesRepository amenitiesRepository)
        {
            this._mapper = mapper;
            this._societyRepository = societyRepository;
            this._amenitiesRepository = amenitiesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSocieties(
            [FromQuery] string? name = null,
            [FromQuery] string? address = null)
        {
            var societyDomain = await _societyRepository.GetSocietiesAsync(name, address);
            return Ok(_mapper.Map<List<SocietyDto>>(societyDomain));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetSocietyById(Guid id)
        {
            var societyDomain = await _societyRepository.GetSocietyByIdAsync(id);
            if (societyDomain == null)
            {
                return NotFound(new
                {
                    message = "Society not found."
                });
            }

            return Ok(_mapper.Map<SocietyDto>(societyDomain));
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateSociety([FromBody] AddSocietyRequestDto addSocietyRequestDto)
        {
            var societyDomain = _mapper.Map<Model.Domain.Society>(addSocietyRequestDto);
            societyDomain.Id = Guid.NewGuid();
            societyDomain.CreatedAt = DateTime.UtcNow;
            societyDomain.UpdatedAt = societyDomain.CreatedAt;
            societyDomain.AmenityIds ??= new List<Guid?>();
            societyDomain.ManagerId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            if (await _societyRepository.SocietyExistsAsync(
                societyDomain.Name,
                societyDomain.Address))
            {
                return Conflict(new
                {
                    message = "A society with the same name and address already exists."
                });
            }

            var amenityIds = societyDomain.AmenityIds
                .Where(amenityId => amenityId.HasValue)
                .Select(amenityId => amenityId!.Value);
            var missingAmenityIds = await _amenitiesRepository.GetMissingAmenityIdsAsync(amenityIds);

            if (missingAmenityIds.Count > 0)
            {
                return BadRequest(new
                {
                    message = "One or more amenity IDs do not exist.",
                    missingAmenityIds
                });
            }

            await _societyRepository.CreateSocietyAsync(societyDomain);

            return CreatedAtAction(
                nameof(GetSocietyById),
                new { id = societyDomain.Id },
                _mapper.Map<SocietyDto>(societyDomain));
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> UpdateSociety(
            Guid id,
            [FromBody] UpdateSocietyRequestDto updateSocietyRequestDto)
        {
            var managerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(managerId))
            {
                return Unauthorized();
            }

            var existingSociety = await _societyRepository.GetSocietyByIdAsync(id);
            if (existingSociety == null)
            {
                return NotFound(new
                {
                    message = "Society not found."
                });
            }

            if (existingSociety.ManagerId != managerId)
            {
                return Forbid();
            }

            if (await _societyRepository.SocietyExistsAsync(
                updateSocietyRequestDto.Name,
                updateSocietyRequestDto.Address,
                id))
            {
                return Conflict(new
                {
                    message = "A society with the same name and address already exists."
                });
            }

            var amenityIds = (updateSocietyRequestDto.AmenityIds ?? new List<Guid?>())
                .Where(amenityId => amenityId.HasValue)
                .Select(amenityId => amenityId!.Value);
            var missingAmenityIds = await _amenitiesRepository.GetMissingAmenityIdsAsync(amenityIds);

            if (missingAmenityIds.Count > 0)
            {
                return BadRequest(new
                {
                    message = "One or more amenity IDs do not exist.",
                    missingAmenityIds
                });
            }

            _mapper.Map(updateSocietyRequestDto, existingSociety);
            existingSociety.UpdatedAt = DateTime.UtcNow;

            await _societyRepository.UpdateSocietyAsync(existingSociety);

            return Ok(_mapper.Map<SocietyDto>(existingSociety));
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteSociety(Guid id)
        {
            var managerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(managerId))
            {
                return Unauthorized();
            }

            var existingSociety = await _societyRepository.GetSocietyByIdAsync(id);
            if (existingSociety == null)
            {
                return NotFound(new
                {
                    message = "Society not found."
                });
            }

            if (existingSociety.ManagerId != managerId)
            {
                return Forbid();
            }

            await _societyRepository.DeleteSocietyAsync(existingSociety);

            return NoContent();
        }
    }
}
