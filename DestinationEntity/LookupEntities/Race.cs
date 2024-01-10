using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class Race : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Staff> Staffs { get; set; }
    }

    class RaceConfig : IEntityTypeConfiguration<Race>
    {
        public void Configure(EntityTypeBuilder<Race> entity)
        {
            entity.HasData(
                new Race { Id = 1, Name = "Asian", DisplayOrder = 1, Enabled = true },
                new Race { Id = 2, Name = "American Indian or Native Alaskan", DisplayOrder = 2, Enabled = true },
                new Race { Id = 3, Name = "Black or African American", DisplayOrder = 3, Enabled = true },
                new Race { Id = 4, Name = "(Formerly) Hispanic or Latino", DisplayOrder = 4, Enabled = true },
                new Race { Id = 5, Name = "Native Hawaiian or Other Pacific Islander", DisplayOrder = 5, Enabled = true },
                new Race { Id = 6, Name = "White", DisplayOrder = 6, Enabled = true },
                new Race { Id = 7, Name = "Unknown", DisplayOrder = 7, Enabled = true },
                new Race { Id = 8, Name = "Two or More Races Reported", DisplayOrder = 8, Enabled = true },
                new Race { Id = 9, Name = "Other", DisplayOrder = 9, Enabled = true }
            );
        }
    }
}