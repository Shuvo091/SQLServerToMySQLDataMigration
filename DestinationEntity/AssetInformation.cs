using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class AssetInformation
    {
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public int? AssetId { get; set; }
        public int? ClientId { get; set; }
        public virtual Asset Asset { get; set; }
        public virtual Client Client { get; set; }

    }

    class AssetInformationConfig : IEntityTypeConfiguration<AssetInformation>
    {
        public void Configure(EntityTypeBuilder<AssetInformation> entity)
        {
        }
    }
}