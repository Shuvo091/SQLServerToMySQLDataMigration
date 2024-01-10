using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class Pet : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class PetConfig : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> entity)
        {
            entity.HasData(
                new Pet { Id = 1, Name = "Cat", DisplayOrder = 1, Enabled = true },
                new Pet { Id = 2, Name = "Bird", DisplayOrder = 2, Enabled = true },
                new Pet { Id = 3, Name = "Aggressive Dog", DisplayOrder = 3, Enabled = true },
                new Pet { Id = 4, Name = "Large Dog", DisplayOrder = 4, Enabled = true },
                new Pet { Id = 5, Name = "Small Dog", DisplayOrder = 5, Enabled = true },
                new Pet { Id = 6, Name = "Other", DisplayOrder = 6, Enabled = true }
            );
        }
    }
}
