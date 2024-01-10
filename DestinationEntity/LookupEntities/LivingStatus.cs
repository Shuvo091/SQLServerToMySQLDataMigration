using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class LivingStatus : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
    }

    class LivingStatusConfig : IEntityTypeConfiguration<LivingStatus>
    {
        public void Configure(EntityTypeBuilder<LivingStatus> entity)
        {
            entity.HasData(
                new LivingStatus { Id = 1, Name = "01-Alone", DisplayOrder = 1, Enabled = true },
                new LivingStatus { Id = 2, Name = "02-Spouse", DisplayOrder = 2, Enabled = true },
                new LivingStatus { Id = 3, Name = "03-Child", DisplayOrder = 3, Enabled = true },
                new LivingStatus { Id = 4, Name = "04-Other Relative", DisplayOrder = 4, Enabled = true },
                new LivingStatus { Id = 5, Name = "05-Non-Relative", DisplayOrder = 5, Enabled = true },
                new LivingStatus { Id = 6, Name = "06-Spouse and Child", DisplayOrder = 6, Enabled = true }
            );
        }
    }
}