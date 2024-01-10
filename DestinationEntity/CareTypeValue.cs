namespace FSSRC_DataMigration.DestinationEntity
{
    public class CareTypeValue
    {
        public int Id { get; set; }
        public int HomeCareRecordId { get; set; }
        public int CareServiceId { get; set; }
        public int? MinimumTimes { get; set; }
        public string? SpecialInstruction { get; set; }
    }
}
