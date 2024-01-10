using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class LivingArrangement : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
    }

    class LivingArrangementConfig : IEntityTypeConfiguration<LivingArrangement>
    {
        public void Configure(EntityTypeBuilder<LivingArrangement> entity)
        {
            entity.HasData(
                new LivingArrangement { Id = 1, Name = "01-Own Home", DisplayOrder = 1, Enabled = true },
                new LivingArrangement { Id = 2, Name = "02-Home of Relative", DisplayOrder = 2, Enabled = true },
                new LivingArrangement { Id = 3, Name = "03-Apt. or Housing for Elderly", DisplayOrder = 3, Enabled = true },
                new LivingArrangement { Id = 4, Name = "04-Nursing Home", DisplayOrder = 4, Enabled = true },
                new LivingArrangement { Id = 5, Name = "05-Hospital", DisplayOrder = 5, Enabled = true },
                new LivingArrangement { Id = 6, Name = "06-Other", DisplayOrder = 6, Enabled = true }
            );
        }
    }
}