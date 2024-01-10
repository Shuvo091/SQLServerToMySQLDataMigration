namespace FSSRC_DataMigration.DestinationEntity
{
    public class InServiceInformation
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public bool Enabled { get; set; }
        public DateTime Date { get; set; }
        public int InServiceTypeId { get; set; }
        public double Hours { get; set; }
        public double PayRate { get; set; }
        public string? Note { get; set; }
        public string? EnteredById { get; set; }
        public DateTime? EnteredOn { get; set; }
    }
}
