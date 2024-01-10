using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class StaffDistrict
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public int DistrictId { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual District District { get; set; }

    }

    class StaffDistrictConfig : IEntityTypeConfiguration<StaffDistrict>
    {
        public void Configure(EntityTypeBuilder<StaffDistrict> entity)
        {
        }
    }
}