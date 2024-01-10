using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class Ethnicity : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
    }

    class EthnicityConfig : IEntityTypeConfiguration<Ethnicity>
    {
        public void Configure(EntityTypeBuilder<Ethnicity> entity)
        {
            entity.HasData(
                new Ethnicity { Id = 1, Name = "Hispanic or Latino", DisplayOrder = 1, Enabled = true },
                new Ethnicity { Id = 2, Name = "Not Hispanic or Latino", DisplayOrder = 2, Enabled = true },
                new Ethnicity { Id = 3, Name = "Unknown", DisplayOrder = 3, Enabled = true }
            );
        }
    }
}