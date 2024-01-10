namespace FSSRC_DataMigration.DestinationEntity
{
    public class Expense
    {
        public int Id { get; set; }
        public int HomeCareRecordId { get; set; }
        public bool Enabled { get; set; }
        public DateTime? Date { get; set; }
        public int? ExpenseTypeId { get; set; }
        public int? Quantity { get; set; }
        public double? UnitCost { get; set; }
        public string? Note { get; set; }
    }
}
