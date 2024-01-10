namespace FSSRC_DataMigration.DestinationEntity
{
    public class TransportationActivity
    {
        public int Id { get; set; }
        public int TransportationRecordId { get; set; }
        public bool Enabled { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Time { get; set; }
        public int? StaffId { get; set; }
        public int? ModeId { get; set; }
        public int? ReasonId { get; set; }
        public int? RideId { get; set; }
        public int? DestinationId { get; set; }
        public bool? IsLateRequest { get; set; }
    }
}
