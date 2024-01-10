namespace FSSRC_DataMigration.DestinationEntity
{
    public class EmergencyContact
    {
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int RelationshipId { get; set; }
        public string? Location { get; set; }
        public bool ROI { get; set; }
        public DateTime? ROIExpirationDate { get; set; }
        public string? Notes { get; set; }
        public int? ClientId { get; set; }
        public int? StaffId { get; set; }
    }
}