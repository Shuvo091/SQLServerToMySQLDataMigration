using FSSRC_DataMigration.DestinationEntity.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class SubService : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int ServiceId { get; set; }
    }

    class SubServiceConfig : IEntityTypeConfiguration<SubService>
    {
        public void Configure(EntityTypeBuilder<SubService> entity)
        {
            entity.HasData(
                new SubService { Id = 1, Code = "Outreach", Description = "Outreach or SIS", Enabled = true, DisplayOrder = 1, ServiceId = (int)ServiceTypeEnum.CounselingAndAdvocacy },
                new SubService { Id = 2, Code = "Counseling", Description = "Supportive or Options Counseling", Enabled = true, DisplayOrder = 2, ServiceId = (int)ServiceTypeEnum.CounselingAndAdvocacy },
                new SubService { Id = 3, Code = "Caregiver", Description = "Piatt- Caregiver", Enabled = true, DisplayOrder = 3, ServiceId = (int)ServiceTypeEnum.CounselingAndAdvocacy },
                new SubService { Id = 4, Code = "GRG", Description = "Piatt- Grandparents raising grandchildren", Enabled = true, DisplayOrder = 4, ServiceId = (int)ServiceTypeEnum.CounselingAndAdvocacy }
            );
        }
    }
}