using System.ComponentModel.DataAnnotations;

namespace SocietySearch.Server.Model.DTO
{
    public class AddAmenitiesDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
