using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class Fund : BaseLookupEntity
    {
        public int Id { get; set; }
        public string FundNumber { get; set; }
        public string FundName { get; set; }
        public double BillRate { get; set; }
        public virtual List<HomeCareRecord> HomeCareRecords { get; set; }
    }

    class FundConfig : IEntityTypeConfiguration<Fund>
    {
        public void Configure(EntityTypeBuilder<Fund> entity)
        {
            entity.HasData(
                new Fund { Id = 1, FundNumber = "00", FundName = "Error", BillRate = 100, DisplayOrder = 1, Enabled = true },
                new Fund { Id = 2, FundNumber = "01", FundName = "Private", BillRate = 100, DisplayOrder = 2, Enabled = true },
                new Fund { Id = 3, FundNumber = "02", FundName = "CNO", BillRate = 100, DisplayOrder = 3, Enabled = true },
                new Fund { Id = 4, FundNumber = "03", FundName = "HA MCO", BillRate = 100, DisplayOrder = 4, Enabled = true },
                new Fund { Id = 5, FundNumber = "04", FundName = "Molina", BillRate = 100, DisplayOrder = 5, Enabled = true },
                new Fund { Id = 6, FundNumber = "05", FundName = "Meridian", BillRate = 100, DisplayOrder = 6, Enabled = true },
                new Fund { Id = 7, FundNumber = "07", FundName = "Blue Cross", BillRate = 100, DisplayOrder = 7, Enabled = true },
                new Fund { Id = 8, FundNumber = "08", FundName = "Illinicare", BillRate = 100, DisplayOrder = 8, Enabled = true },
                new Fund { Id = 9, FundNumber = "09", FundName = "Aetna", BillRate = 100, DisplayOrder = 9, Enabled = true },
                new Fund { Id = 10, FundNumber = "60", FundName = "Co-Pay", BillRate = 100, DisplayOrder = 10, Enabled = true },
                new Fund { Id = 11, FundNumber = "61", FundName = "CCP", BillRate = 100, DisplayOrder = 11, Enabled = true },
                new Fund { Id = 12, FundNumber = "65", FundName = "MISC(OTHER)", BillRate = 100, DisplayOrder = 12, Enabled = true },
                new Fund { Id = 13, FundNumber = "999", FundName = "SCC University of IL", BillRate = 100, DisplayOrder = 13, Enabled = true },
                new Fund { Id = 14, FundNumber = "1000", FundName = "SCC Busey", BillRate = 100, DisplayOrder = 14, Enabled = true },
                new Fund { Id = 15, FundNumber = "1010", FundName = "SCC Carle", BillRate = 100, DisplayOrder = 15, Enabled = true }
            );
        }
    }
}

