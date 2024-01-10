using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class InfoVia : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class InfoViaConfig : IEntityTypeConfiguration<InfoVia>
    {
        public void Configure(EntityTypeBuilder<InfoVia> entity)
        {
            entity.HasData(
                new InfoVia { Id = 1, Name = "Phone", DisplayOrder = 1, Enabled = true },
                new InfoVia { Id = 2, Name = "Conference", DisplayOrder = 2, Enabled = true },
                new InfoVia { Id = 3, Name = "Home Visit", DisplayOrder = 3, Enabled = true },
                new InfoVia { Id = 4, Name = "Case Manager", DisplayOrder = 4, Enabled = true },
                new InfoVia { Id = 5, Name = "Nurse", DisplayOrder = 5, Enabled = true },
                new InfoVia { Id = 6, Name = "Face to Face", DisplayOrder = 6, Enabled = true },
                new InfoVia { Id = 7, Name = "Preparation", DisplayOrder = 7, Enabled = true }
            );
        }
    }
}

