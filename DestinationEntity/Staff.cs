namespace FSSRC_DataMigration.DestinationEntity
{
    public class Staff
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? StatusId { get; set; }
        public string? Tips { get; set; }
        public string? HomeAddress { get; set; }
        public string? City { get; set; }
        public int? StateId { get; set; }
        public string? ZipCode { get; set; }
        public int? CountyId { get; set; }
        public string? Email { get; set; }
        public string? HomePhone { get; set; }
        public string? SecondaryHomePhone { get; set; }
        public string? CellPhone { get; set; }
        public string? WorkPhone { get; set; }
        public DateTime? Birthdate { get; set; }
        public int? GenderId { get; set; }
        public int? RaceId { get; set; }
        public string? SocialSecurity { get; set; }
        public DateTime? InsuranceExpiration { get; set; }
        public DateTime? DriversLicenseExpiration { get; set; }
        public double? PayrollRate { get; set; }
    }
}
