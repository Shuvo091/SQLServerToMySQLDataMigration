using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class Service : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

    class ServiceConfig : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> entity)
        {
            entity.HasData(
                new SIS { Id = 1, Code = "B10", Description = "B10", DisplayOrder = 1, Enabled = true },
                new SIS { Id = 2, Code = "E10", Description = "E01", DisplayOrder = 2, Enabled = true }
            );
        }
    }
}