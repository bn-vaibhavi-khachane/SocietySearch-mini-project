namespace SocietySearch.Server.Model.DTO
{
    public class AddSocietyRequestDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Summary { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string EstablishmentYear { get; set; }
        public string SocietyImageUrl { get; set; }

        // Ids of amenities associated with this society, stored as JSON
        public List<Guid?> AmenityIds { get; set; }
    }
}
