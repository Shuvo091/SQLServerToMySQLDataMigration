using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class District : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<StaffDistrict> StaffDistricts { get; set; }
    }

    class DistrictConfig : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> entity)
        {
            entity.HasData(
                new District { Id = 1, Code = "DTU", Name = "Downtown Urbana", DisplayOrder = 1, Enabled = true },
                new District { Id = 2, Code = "SU", Name = "South Urbana", DisplayOrder = 2, Enabled = true },
                new District { Id = 3, Code = "FH", Name = "Florida House", DisplayOrder = 3, Enabled = true },
                new District { Id = 4, Code = "SCM", Name = "Sunnycrest Manor", DisplayOrder = 4, Enabled = true },
                new District { Id = 5, Code = "SEU", Name = "South East Urbana", DisplayOrder = 5, Enabled = true },
                new District { Id = 6, Code = "NEU", Name = "North East Urbana", DisplayOrder = 6, Enabled = true },
                new District { Id = 7, Code = "EU", Name = "East Urbana", DisplayOrder = 7, Enabled = true },
                new District { Id = 8, Code = "NU", Name = "North Urbana", DisplayOrder = 8, Enabled = true },
                new District { Id = 9, Code = "ECWU", Name = "NE Champaign/NW Urbana", DisplayOrder = 9, Enabled = true },
                new District { Id = 10, Code = "WS", Name = "Washington Square", DisplayOrder = 10, Enabled = true },
                new District { Id = 11, Code = "CC", Name = "Central Champaign", DisplayOrder = 11, Enabled = true },
                new District { Id = 12, Code = "RB", Name = "Round Barn Manor", DisplayOrder = 12, Enabled = true },
                new District { Id = 13, Code = "NWC", Name = "North West Champaign", DisplayOrder = 13, Enabled = true },
                new District { Id = 14, Code = "WC", Name = "West Champaign", DisplayOrder = 14, Enabled = true },
                new District { Id = 15, Code = "SWC", Name = "South West Champaign", DisplayOrder = 15, Enabled = true },
                new District { Id = 16, Code = "SV", Name = "Savoy", DisplayOrder = 16, Enabled = true },
                new District { Id = 17, Code = "TPP", Name = "Tolono/Philo/Pesotum", DisplayOrder = 17, Enabled = true },
                new District { Id = 18, Code = "SJO", Name = "St Joseph/Ogden", DisplayOrder = 18, Enabled = true },
                new District { Id = 19, Code = "TBR", Name = "Thomasboro/Rantoul", DisplayOrder = 19, Enabled = true },
                new District { Id = 20, Code = "MHS", Name = "Mahomet/Seymour", DisplayOrder = 20, Enabled = true },
                new District { Id = 21, Code = "EOM", Name = "Edge Of The Mall", DisplayOrder = 21, Enabled = true }
            );
        }
    }
}