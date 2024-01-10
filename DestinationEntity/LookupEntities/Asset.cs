using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class Asset : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class AssetConfig : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> entity)
        {
            entity.HasData(
                new Asset { Id = 1, Name = "Checking", DisplayOrder = 1, Enabled = true },
                new Asset { Id = 2, Name = "Savings", DisplayOrder = 2, Enabled = true },
                new Asset { Id = 3, Name = "Annuity", DisplayOrder = 3, Enabled = true },
                new Asset { Id = 4, Name = "CD", DisplayOrder = 4, Enabled = true },
                new Asset { Id = 5, Name = "Interest Income", DisplayOrder = 5, Enabled = true },
                new Asset { Id = 6, Name = "Stocks/Bonds", DisplayOrder = 6, Enabled = true },
                new Asset { Id = 7, Name = "Burial Trust", DisplayOrder = 7, Enabled = true },
                new Asset { Id = 8, Name = "Life Insurance Cash Value", DisplayOrder = 8, Enabled = true },
                new Asset { Id = 9, Name = "Reverse Mortage", DisplayOrder = 9, Enabled = true },
                new Asset { Id = 10, Name = "Other", DisplayOrder = 10, Enabled = true }
            );
        }
    }
}