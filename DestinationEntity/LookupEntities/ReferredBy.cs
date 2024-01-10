using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class ReferredBy : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
    }

    class ReferredByConfig : IEntityTypeConfiguration<ReferredBy>
    {
        public void Configure(EntityTypeBuilder<ReferredBy> entity)
        {
            entity.HasData(
                new ReferredBy { Id = 1, Name = "Agency (Specify)", DisplayOrder = 1, Enabled = true },
                new ReferredBy { Id = 2, Name = "Doctor/Medical Professional", DisplayOrder = 2, Enabled = true },
                new ReferredBy { Id = 3, Name = "Family/Friend", DisplayOrder = 3, Enabled = true },
                new ReferredBy { Id = 4, Name = "Media (Newspaper/TV/Radio)", DisplayOrder = 4, Enabled = true },
                new ReferredBy { Id = 5, Name = "Website", DisplayOrder = 5, Enabled = true },
                new ReferredBy { Id = 6, Name = "Social Media", DisplayOrder = 6, Enabled = true },
                new ReferredBy { Id = 7, Name = "Phone Book", DisplayOrder = 7, Enabled = true },
                new ReferredBy { Id = 8, Name = "Other (Specify)", DisplayOrder = 8, Enabled = true },
                new ReferredBy { Id = 9, Name = "Unknown", DisplayOrder = 9, Enabled = true }
            );
        }
    }


}