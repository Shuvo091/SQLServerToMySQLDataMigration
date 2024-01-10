using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class ProgramInfo : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public int SubServiceId { get; set; }
    }

    class ProgramInfoConfig : IEntityTypeConfiguration<ProgramInfo>
    {
        public void Configure(EntityTypeBuilder<ProgramInfo> entity)
        {
            entity.HasData(
                new ProgramInfo { Id = 1, Code = "PEARLS", Description = "PEARLS", Note = "", SubServiceId = 2, Enabled = true, DisplayOrder = 1 },
                new ProgramInfo { Id = 2, Code = "Friendly Callers", Description = "Friendly Callers", Note = "", SubServiceId = 2, Enabled = true, DisplayOrder = 2 },
                new ProgramInfo { Id = 3, Code = "Activity Box", Description = "Activity Box", Note = "", SubServiceId = 2, Enabled = true, DisplayOrder = 3 }
            );
        }
    }
}