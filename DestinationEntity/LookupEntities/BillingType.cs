using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class BillingType : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class BillingTypeConfig : IEntityTypeConfiguration<BillingType>
    {
        public void Configure(EntityTypeBuilder<BillingType> entity)
        {
            entity.HasData(
                new BillingType { Id = 1, Name = "CNO, $15.50", DisplayOrder = 1, Enabled = true },
                new BillingType { Id = 2, Name = "Hourly, $25.00", DisplayOrder = 2, Enabled = true },
                new BillingType { Id = 3, Name = "Other, $20.00", DisplayOrder = 3, Enabled = true },
                new BillingType { Id = 4, Name = "Overtime, $30.00", DisplayOrder = 4, Enabled = true },
                new BillingType { Id = 5, Name = "SSC - 1 or 2 Children, $25.00", DisplayOrder = 5, Enabled = true },
                new BillingType { Id = 6, Name = "SSC - 3 Children, $26.00", DisplayOrder = 6, Enabled = true },
                new BillingType { Id = 7, Name = "SSC - 4 Children, $27.00", DisplayOrder = 7, Enabled = true },
                new BillingType { Id = 8, Name = "SSC - 5 or More Children, $50.00", DisplayOrder = 8, Enabled = true },
                new BillingType { Id = 9, Name = "SSC - Cancelled Late Fee, $76.00", DisplayOrder = 9, Enabled = true }
            );
        }
    }
}

