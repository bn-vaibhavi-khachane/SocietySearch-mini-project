namespace SocietySearch.Server.Model.DTO
{
    public class UnitDto
    {
        public Guid Id { get; set; }
        public Guid SocietyId { get; set; }
        public string Type { get; set; } = string.Empty;
        public int AvailableUnits { get; set; }
        public bool AvailabilityStatus { get; set; }
    }
}