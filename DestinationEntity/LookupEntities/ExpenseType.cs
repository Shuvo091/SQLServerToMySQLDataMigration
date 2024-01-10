using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class ExpenseType : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double? UnitCost { get; set; }
    }

    class ExpenseTypeConfig : IEntityTypeConfiguration<ExpenseType>
    {
        public void Configure(EntityTypeBuilder<ExpenseType> entity)
        {
            entity.HasData(
                new ExpenseType { Id = 1, Name = "Mileage", UnitCost = 0.50, DisplayOrder = 1, Enabled = true },
                new ExpenseType { Id = 2, Name = "Credit", DisplayOrder = 2, Enabled = true },
                new ExpenseType { Id = 3, Name = "Expense", DisplayOrder = 3, Enabled = true },
                new ExpenseType { Id = 4, Name = "Other", DisplayOrder = 4, Enabled = true }
            );
        }
    }
}