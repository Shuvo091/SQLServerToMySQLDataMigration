namespace FSSRC_DataMigration.DestinationEntity
{
    public class CaseManager : BaseLookupEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Agency { get; set; }
    }
}
