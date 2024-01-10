using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class Reason : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

    class ReasonConfig : IEntityTypeConfiguration<Reason>
    {
        public void Configure(EntityTypeBuilder<Reason> entity)
        {
            entity.HasData(
                new Reason { Id = 1, Code = "BUSINESS", Description = "Misc. Personal Business", DisplayOrder = 1, Enabled = true },
                new Reason { Id = 2, Code = "GROCERY", Description = "Grocery Store", DisplayOrder = 2, Enabled = true },
                new Reason { Id = 3, Code = "MEDICAL", Description = "Misc. Medical Appointments", DisplayOrder = 3, Enabled = true },
                new Reason { Id = 4, Code = "Q.O.L.", Description = "Quality of Life", DisplayOrder = 4, Enabled = true }
            );
        }
    }
}