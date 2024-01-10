namespace FSSRC_DataMigration.DestinationEntity
{
    public class ActualEvent
    {
        public int Id { get; set; }
        public int? StaffId { get; set; }
        public int? ClientId { get; set; }
        public double? BillRate { get; set; }
        public double? PayrollRate { get; set; }
        public int? FollowingID { get; set; }
        public string? Guid { get; set; }
        public string? Subject { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? StartTimezone { get; set; }
        public string? EndTimezone { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public bool IsAllDay { get; set; }
        public int? RecurrenceID { get; set; }
        public string? RecurrenceRule { get; set; }
        public string? RecurrenceException { get; set; }
        public bool IsReadonly { get; set; }
        public bool IsBlock { get; set; }
    }
}
