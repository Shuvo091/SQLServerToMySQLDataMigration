using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class MaritalStatus : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
    }

    class MaritalStatusConfig : IEntityTypeConfiguration<MaritalStatus>
    {
        public void Configure(EntityTypeBuilder<MaritalStatus> entity)
        {
            entity.HasData(
                new MaritalStatus { Id = 1, Name = "01-Never Married", DisplayOrder = 1, Enabled = true },
                new MaritalStatus { Id = 2, Name = "02-Married", DisplayOrder = 2, Enabled = true },
                new MaritalStatus { Id = 3, Name = "03-Separated", DisplayOrder = 3, Enabled = true },
                new MaritalStatus { Id = 4, Name = "04-Divorced", DisplayOrder = 4, Enabled = true },
                new MaritalStatus { Id = 5, Name = "05-Widowed", DisplayOrder = 5, Enabled = true },
                new MaritalStatus { Id = 6, Name = "06-Minor Child", DisplayOrder = 6, Enabled = true },
                new MaritalStatus { Id = 7, Name = "07-Unknown", DisplayOrder = 7, Enabled = true }
            );
        }
    }
}

