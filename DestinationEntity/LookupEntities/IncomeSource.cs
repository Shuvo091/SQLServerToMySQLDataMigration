using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class IncomeSource : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class IncomeSourceConfig : IEntityTypeConfiguration<IncomeSource>
    {
        public void Configure(EntityTypeBuilder<IncomeSource> entity)
        {
            entity.HasData(
                new IncomeSource { Id = 1, Name = "01-Social Security", DisplayOrder = 1, Enabled = true },
                new IncomeSource { Id = 2, Name = "02-DPA", DisplayOrder = 2, Enabled = true },
                new IncomeSource { Id = 3, Name = "03-SSI", DisplayOrder = 3, Enabled = true },
                new IncomeSource { Id = 4, Name = "05-Other", DisplayOrder = 4, Enabled = true },
                new IncomeSource { Id = 5, Name = "06-Unknown", DisplayOrder = 5, Enabled = true }
            );
        }
    }
}