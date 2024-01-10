namespace FSSRC_DataMigration.DestinationEntity
{
    public class BaseServiceRecord
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int? StatusId { get; set; }
        public string? Tips { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
    }
}
