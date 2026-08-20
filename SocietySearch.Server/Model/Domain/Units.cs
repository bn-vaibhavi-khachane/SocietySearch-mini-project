using SocietySearch.Server.Validation;

namespace SocietySearch.Server.Model.Domain
{
    public class Units
    {
        public Guid Id { get; set; }
        public Guid SocietyId { get; set; }
        [AllowedValues("1 BHK", "2 BHK", "3 BHK", "4 BHK", "Penthouse", "Studio")]
        
        public string Type { get; set; } = string.Empty;
        public int AvailableUnits { get; set; }
        public bool AvailabilityStatus { get; set; }

        //Navigation Property
        public Society Society { get; set; } = null!;
    }
    

}
