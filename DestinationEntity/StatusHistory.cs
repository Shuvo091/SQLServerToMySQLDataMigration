namespace FSSRC_DataMigration.DestinationEntity
{
    public class StatusHistory
    {
        public int Id { get; set; }
        public int? StatusId { get; set; }
        public int? HomeCareRecordId { get; set; }
        public int? MealsOnWheelsRecordId { get; set; }
        public int? TransportationRecordId { get; set; }
        public int? CounselingRecordId { get; set; }
        public int? APSRecordId { get; set; }
        public int? SubServiceId { get; set; }
        public int? ProgramInfoId { get; set; }
        public DateTime Date { get; set; }
        public int? StatusReasonId { get; set; }
        public Guid? EnteredById { get; set; }
        public DateTime EnteredOn { get; set; }
    }
}
