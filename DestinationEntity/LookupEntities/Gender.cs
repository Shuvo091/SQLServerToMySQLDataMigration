using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class Gender : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Staff> Staffs { get; set; }
    }

    class GenderConfig : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> entity)
        {
            entity.HasData(
                new Gender { Id = 1, Name = "Male", DisplayOrder = 1, Enabled = true },
                new Gender { Id = 2, Name = "Female", DisplayOrder = 2, Enabled = true },
                new Gender { Id = 3, Name = "Other", DisplayOrder = 3, Enabled = true }
            );
        }
    }
}