using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class Mode : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

    class ModeConfig : IEntityTypeConfiguration<Mode>
    {
        public void Configure(EntityTypeBuilder<Mode> entity)
        {
            entity.HasData(
                new Mode { Id = 1, Code = "aTAXI Orange", Description = "Taxi (Orange Taxi)", DisplayOrder = 1, Enabled = true },
                new Mode { Id = 2, Code = "bVOL", Description = "Volunteers Vehicle", DisplayOrder = 2, Enabled = true },
                new Mode { Id = 3, Code = "cTAXI Yellow", Description = "Taxi (Yellow Taxi)", DisplayOrder = 3, Enabled = true },
                new Mode { Id = 4, Code = "dAtlas", Description = "Taxi (Atlas)", DisplayOrder = 4, Enabled = true },
                new Mode { Id = 5, Code = "ORANGE", Description = "Orange", DisplayOrder = 5, Enabled = true },
                new Mode { Id = 6, Code = "VOL", Description = "Volunteer", DisplayOrder = 6, Enabled = true },
                new Mode { Id = 7, Code = "zCancelled", Description = "Cancelled", DisplayOrder = 7, Enabled = true },
                new Mode { Id = 8, Code = "zCORKYS", Description = "Corky's Limousine Service", DisplayOrder = 8, Enabled = true },
                new Mode { Id = 9, Code = "zCUDELIVERY", Description = "C-U Delivery Service", DisplayOrder = 9, Enabled = true },
                new Mode { Id = 10, Code = "zHC-CCP", Description = "HomeCare-CCP", DisplayOrder = 10, Enabled = true },
                new Mode { Id = 11, Code = "zHC-PP", Description = "HomeCare-Private Pay", DisplayOrder = 11, Enabled = true },
                new Mode { Id = 12, Code = "zMOMMIES", Description = "Mommies Cab", DisplayOrder = 12, Enabled = true },
                new Mode { Id = 13, Code = "zNORIDE", Description = "Could NOT Fill Transportation Request", DisplayOrder = 13, Enabled = true },
                new Mode { Id = 14, Code = "zORANGE", Description = "Orange Cab", DisplayOrder = 14, Enabled = true },
                new Mode { Id = 15, Code = "zSPECCARE", Description = "Special Care", DisplayOrder = 15, Enabled = true },
                new Mode { Id = 16, Code = "zSTAFF", Description = "Staff Vehicle", DisplayOrder = 16, Enabled = true }
            );
        }
    }
}