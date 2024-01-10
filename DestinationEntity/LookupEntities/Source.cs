using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class Source : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class SourceConfig : IEntityTypeConfiguration<Source>
    {
        public void Configure(EntityTypeBuilder<Source> entity)
        {
            entity.HasData(
                new Source { Id = 1, Name = "Staff", DisplayOrder = 1, Enabled = true },
                new Source { Id = 2, Name = "Client", DisplayOrder = 2, Enabled = true },
                new Source { Id = 3, Name = "Homemaker", DisplayOrder = 3, Enabled = true },
                new Source { Id = 4, Name = "Case Manager", DisplayOrder = 4, Enabled = true },
                new Source { Id = 5, Name = "Family Member", DisplayOrder = 5, Enabled = true }
            );
        }
    }
}

