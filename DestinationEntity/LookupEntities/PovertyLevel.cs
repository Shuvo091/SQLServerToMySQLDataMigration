using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class PovertyLevel : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
    }

    class PovertyLevelConfig : IEntityTypeConfiguration<PovertyLevel>
    {
        public void Configure(EntityTypeBuilder<PovertyLevel> entity)
        {
            entity.HasData(
                new PovertyLevel { Id = 1, Name = "Below", DisplayOrder = 1, Enabled = true },
                new PovertyLevel { Id = 2, Name = "Below 125% of Poverty", DisplayOrder = 2, Enabled = true },
                new PovertyLevel { Id = 3, Name = "Below 150% of Poverty", DisplayOrder = 3, Enabled = true },
                new PovertyLevel { Id = 4, Name = "Above 150% of Poverty", DisplayOrder = 4, Enabled = true }
            );
        }
    }
}