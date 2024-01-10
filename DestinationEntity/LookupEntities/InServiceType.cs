using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class InServiceType : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class InServiceTypeConfig : IEntityTypeConfiguration<InServiceType>
    {
        public void Configure(EntityTypeBuilder<InServiceType> entity)
        {
            entity.HasData(
                new InServiceType { Id = 1, Name = "In-Service All HCAs", DisplayOrder = 1, Enabled = true },
                new InServiceType { Id = 2, Name = "Show Up Pay", DisplayOrder = 2, Enabled = true },
                new InServiceType { Id = 3, Name = "Pre-Service HCA", DisplayOrder = 3, Enabled = true },
                new InServiceType { Id = 4, Name = "Makeup In-Service All HCAs", DisplayOrder = 4, Enabled = true }
            );
        }
    }
}