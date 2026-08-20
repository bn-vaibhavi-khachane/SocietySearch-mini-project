using SocietySearch.Server.Validation;
using System.ComponentModel.DataAnnotations;

namespace SocietySearch.Server.Model.DTO
{
    public class UpdateUnitRequestDto
    {
        [Required]
        [Validation.AllowedValues("1 BHK", "2 BHK", "3 BHK", "4 BHK", "Penthouse", "Studio")]
        public string Type { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int AvailableUnits { get; set; }

        public bool AvailabilityStatus { get; set; }
    }
}