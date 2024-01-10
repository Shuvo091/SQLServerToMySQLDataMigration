using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class County : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Staff> Staffs { get; set; }
    }

    class CountyConfig : IEntityTypeConfiguration<County>
    {
        public void Configure(EntityTypeBuilder<County> entity)
        {
            entity.HasData(
                new County { Id = 1, Name = "Champaign", DisplayOrder = 1, Enabled = true },
                new County { Id = 2, Name = "Dewitt", DisplayOrder = 2, Enabled = true },
                new County { Id = 3, Name = "Ford", DisplayOrder = 3, Enabled = true },
                new County { Id = 4, Name = "Piatt", DisplayOrder = 4, Enabled = true },
                new County { Id = 5, Name = "Vermillion", DisplayOrder = 5, Enabled = true },
                new County { Id = 6, Name = "Other", DisplayOrder = 6, Enabled = true }
            );
        }
    }
}