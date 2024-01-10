using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class Ride : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class RideConfig : IEntityTypeConfiguration<Ride>
    {
        public void Configure(EntityTypeBuilder<Ride> entity)
        {
            entity.HasData(
                new Ride { Id = 1, Name = "One Way", DisplayOrder = 1, Enabled = true },
                new Ride { Id = 2, Name = "Round Trip", DisplayOrder = 2, Enabled = true },
                new Ride { Id = 3, Name = "Special Round Trip - 1/2", DisplayOrder = 3, Enabled = true },
                new Ride { Id = 4, Name = "Special Round Trip", DisplayOrder = 4, Enabled = true }
            );
        }
    }
}