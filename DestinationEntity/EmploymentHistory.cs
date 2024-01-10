namespace FSSRC_DataMigration.DestinationEntity
{
    public class EmploymentHistory
    {
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public DateTime? DateHired { get; set; }
        public DateTime? TermDate { get; set; }
        public string? ReasonForTermination { get; set; }
        public int StaffId { get; set; }
    }
}