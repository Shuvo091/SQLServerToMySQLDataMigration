namespace FSSRC_DataMigration.DestinationEntity
{
    public class IncomeInformation
    {
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public Decimal? MonthlyIncome { get; set; }
        public Decimal? YearlyIncome { get; set; }
        public int? IncomeSourceId { get; set; }
        public int? ClientId { get; set; }
    }
}