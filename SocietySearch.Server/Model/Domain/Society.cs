namespace SocietySearch.Server.Model.Domain
{
    public class Society
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Summary { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string EstablishmentYear  { get; set; }
        public string SocietyImageUrl { get; set; }

        // Ids of amenities associated with this society, stored as JSON
        public List<Guid?> AmenityIds { get; set; }
        public string ManagerId { get; set; } = string.Empty;
        public Manager Manager { get; set; } = null!;
    }
}
