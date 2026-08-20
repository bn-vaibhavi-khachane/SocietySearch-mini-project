using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocietySearch.Server.Model.DTO;
using SocietySearch.Server.Repositories;
using System.Security.Claims;

namespace SocietySearch.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitsRepository _unitsRepository;
        private readonly ISocietyRepository _societyRepository;

        public UnitsController(
            IMapper mapper,
            IUnitsRepository unitsRepository,
            ISocietyRepository societyRepository)
        {
            _mapper = mapper;
            _unitsRepository = unitsRepository;
            _societyRepository = societyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUnits([FromQuery] Guid? societyId = null)
        {
            var units = await _unitsRepository.GetUnitsAsync(societyId);
            return Ok(_mapper.Map<List<UnitDto>>(units));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUnitById(Guid id)
        {
            var unit = await _unitsRepository.GetUnitByIdAsync(id);
            if (unit == null)
            {
                return NotFound(new { message = "Unit not found." });
            }

            return Ok(_mapper.Map<UnitDto>(unit));
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateUnit([FromBody] AddUnitRequestDto addUnitRequestDto)
        {
            var society = await _societyRepository.GetSocietyByIdAsync(addUnitRequestDto.SocietyId);
            if (society == null)
            {
                return BadRequest(new { message = "Society not found." });
            }

            if (!IsSocietyManagedByCurrentUser(society.ManagerId))
            {
                return Forbid();
            }

            var unit = _mapper.Map<Model.Domain.Units>(addUnitRequestDto);
            unit.Id = Guid.NewGuid();

            await _unitsRepository.CreateUnitAsync(unit);

            return CreatedAtAction(
                nameof(GetUnitById),
                new { id = unit.Id },
                _mapper.Map<UnitDto>(unit));
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> UpdateUnit(Guid id, [FromBody] UpdateUnitRequestDto updateUnitRequestDto)
        {
            var unit = await _unitsRepository.GetUnitByIdAsync(id);
            if (unit == null)
            {
                return NotFound(new { message = "Unit not found." });
            }

            var society = await _societyRepository.GetSocietyByIdAsync(unit.SocietyId);
            if (society == null)
            {
                return NotFound(new { message = "Society not found." });
            }

            if (!IsSocietyManagedByCurrentUser(society.ManagerId))
            {
                return Forbid();
            }

            _mapper.Map(updateUnitRequestDto, unit);
            await _unitsRepository.UpdateUnitAsync(unit);

            return Ok(_mapper.Map<UnitDto>(unit));
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteUnit(Guid id)
        {
            var unit = await _unitsRepository.GetUnitByIdAsync(id);
            if (unit == null)
            {
                return NotFound(new { message = "Unit not found." });
            }

            var society = await _societyRepository.GetSocietyByIdAsync(unit.SocietyId);
            if (society == null)
            {
                return NotFound(new { message = "Society not found." });
            }

            if (!IsSocietyManagedByCurrentUser(society.ManagerId))
            {
                return Forbid();
            }

            await _unitsRepository.DeleteUnitAsync(unit);
            return NoContent();
        }

        private bool IsSocietyManagedByCurrentUser(string societyManagerId)
        {
            var managerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return !string.IsNullOrWhiteSpace(managerId) && societyManagerId == managerId;
        }
    }
}