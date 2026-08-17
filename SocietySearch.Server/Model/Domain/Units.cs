namespace SocietySearch.Server.Model.Domain
{
    public class Units
    {
        public Guid Id { get; set; }
        public Guid SocietyId { get; set; }
        public int UnitNumber { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }

        //Navigation Property
        public Society Society { get; set; }
    }
    

}
