using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class Route : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class RouteConfig : IEntityTypeConfiguration<Route>
    {
        public void Configure(EntityTypeBuilder<Route> entity)
        {
            entity.HasData(
                new Route { Id = 1, Name = "Blue (future use)", DisplayOrder = 1, Enabled = true },
                new Route { Id = 2, Name = "Grey", DisplayOrder = 2, Enabled = true },
                new Route { Id = 3, Name = "Orange", DisplayOrder = 3, Enabled = true },
                new Route { Id = 4, Name = "Pink", DisplayOrder = 4, Enabled = true },
                new Route { Id = 5, Name = "Red", DisplayOrder = 5, Enabled = true },
                new Route { Id = 6, Name = "Tan", DisplayOrder = 6, Enabled = true },
                new Route { Id = 7, Name = "Yellow", DisplayOrder = 7, Enabled = true }
            );
        }
    }
}
