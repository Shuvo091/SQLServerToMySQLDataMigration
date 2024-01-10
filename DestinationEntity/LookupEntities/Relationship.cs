using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class Relationship : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class RelationshipConfig : IEntityTypeConfiguration<Relationship>
    {
        public void Configure(EntityTypeBuilder<Relationship> entity)
        {
            entity.HasData(
                new Relationship { Id = 1, Name = "Father", DisplayOrder = 1, Enabled = true },
                new Relationship { Id = 2, Name = "Mother", DisplayOrder = 2, Enabled = true },
                new Relationship { Id = 3, Name = "Daughter", DisplayOrder = 3, Enabled = true },
                new Relationship { Id = 4, Name = "Son", DisplayOrder = 4, Enabled = true },
                new Relationship { Id = 5, Name = "Spouse", DisplayOrder = 5, Enabled = true },
                new Relationship { Id = 6, Name = "Friend", DisplayOrder = 6, Enabled = true },
                new Relationship { Id = 7, Name = "Other", DisplayOrder = 7, Enabled = true }
            );
        }
    }
}