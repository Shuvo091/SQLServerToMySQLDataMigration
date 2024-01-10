using FSSRC_DataMigration.DestinationEntity.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class Status : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Staff> Staffs { get; set; }
    }

    class StatusConfig : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> entity)
        {
            entity.HasData(
                new Status { Id = (int)StatusEnum.Open, Name = AppEnum.GetDisplayName(StatusEnum.Open), DisplayOrder = 1, Enabled = true },
                new Status { Id = (int)StatusEnum.Closed, Name = AppEnum.GetDisplayName(StatusEnum.Closed), DisplayOrder = 2, Enabled = true },
                new Status { Id = (int)StatusEnum.Hold, Name = AppEnum.GetDisplayName(StatusEnum.Hold), DisplayOrder = 3, Enabled = true }
            );
        }
    }
}

