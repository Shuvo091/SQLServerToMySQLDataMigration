namespace FSSRC_DataMigration.DestinationEntity
{
    public class Client
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MI { get; set; }
        public string? PreferredName { get; set; }
        public string? Tips { get; set; }
        public string? HomeAddress { get; set; }
        public string? City { get; set; }
        public int? StateId { get; set; }
        public string? ZipCode { get; set; }
        public int? CountyId { get; set; }
        public string? Email { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? HomePhone { get; set; }
        public string? OtherPhone { get; set; }
        public string? OtherPhoneDesc { get; set; }
        public string? BillingName { get; set; }
        public string? BillingAddress { get; set; }
        public string? BillingCity { get; set; }
        public int? BillingStateId { get; set; }
        public string? SocialSecurity { get; set; }
        public string? Medicaid { get; set; }
        public string? InsuranceId { get; set; }
        public bool? DASHPass { get; set; }
        public int? GenderId { get; set; }
        public int? RaceId { get; set; }
        public int? EthnicityId { get; set; }
        public int? MaritalStatusId { get; set; }
        public int? LivingStatusId { get; set; }
        public int? LivingArrangementId { get; set; }
        public int? PetId { get; set; }
        public int? DistrictId { get; set; }
        public int? PovertyLevelId { get; set; }
        public double? HouseholdIncome { get; set; }
        public string? BillingZip { get; set; }
        public DateTime? IntakeDate { get; set; }
        public string? IntakeStaff { get; set; }
        public int? ReferredById { get; set; }
        public string? IfRefferedByOther { get; set; }
        public string? IntakeNotes { get; set; }
    }
}
