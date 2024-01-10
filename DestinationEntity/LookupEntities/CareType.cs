using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class CareType : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<CareService> CareServices { get; set; }
    }

    class CareTypeConfig : IEntityTypeConfiguration<CareType>
    {
        public void Configure(EntityTypeBuilder<CareType> entity)
        {
            entity.HasData(
                new CareType { Id = 1, Name = "Personal ADL", Enabled = true, DisplayOrder = 1 },
                new CareType { Id = 2, Name = "Home ADL", Enabled = true, DisplayOrder = 2 },
                new CareType { Id = 3, Name = "Medication", Enabled = true, DisplayOrder = 3 },
                new CareType { Id = 4, Name = "Supervision", Enabled = true, DisplayOrder = 4 }
            );
        }
    }
}
