using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class ActivityType : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

    class ActivityTypeConfig : IEntityTypeConfiguration<ActivityType>
    {
        public void Configure(EntityTypeBuilder<ActivityType> entity)
        {
            entity.HasData(
                new ActivityType { Id = 1, Code = "ADVOCACY", Description = "Advocacy", DisplayOrder = 1, Enabled = true },
                new ActivityType { Id = 2, Code = "ADVOCACY HV", Description = "Advocacy Home Visit", DisplayOrder = 2, Enabled = true },
                new ActivityType { Id = 3, Code = "CASE REC", Description = "Case Recording", DisplayOrder = 3, Enabled = true },
                new ActivityType { Id = 4, Code = "CG HV", Description = "Caregiver Home Visit", DisplayOrder = 4, Enabled = true },
                new ActivityType { Id = 5, Code = "CG PC", Description = "Caregiver Phone Call", DisplayOrder = 5, Enabled = true },
                new ActivityType { Id = 6, Code = "CG PREP", Description = "Caregiver Prep", DisplayOrder = 6, Enabled = true },
                new ActivityType { Id = 7, Code = "FTF", Description = "FTF", DisplayOrder = 7, Enabled = true },
                new ActivityType { Id = 8, Code = "GRG HV", Description = "GRG Home Visit", DisplayOrder = 8, Enabled = true },
                new ActivityType { Id = 9, Code = "GRG PC", Description = "GRG Phone Call", DisplayOrder = 9, Enabled = true },
                new ActivityType { Id = 10, Code = "GRG PREP", Description = "GRG Prep", DisplayOrder = 10, Enabled = true },
                new ActivityType { Id = 11, Code = "HOME VISIT", Description = "Home Visit", DisplayOrder = 11, Enabled = true },
                new ActivityType { Id = 12, Code = "IN OFFICE", Description = "In Office Activities", DisplayOrder = 12, Enabled = true },
                new ActivityType { Id = 13, Code = "OFFICE VISIT", Description = "Office Visit", DisplayOrder = 13, Enabled = true },
                new ActivityType { Id = 14, Code = "OUT OFFICE", Description = "Out of Office Activities", DisplayOrder = 14, Enabled = true },
                new ActivityType { Id = 15, Code = "PC", Description = "PC", DisplayOrder = 15, Enabled = true },
                new ActivityType { Id = 16, Code = "PEARLS", Description = "Pearls", DisplayOrder = 16, Enabled = true },
                new ActivityType { Id = 17, Code = "PEARLS HV", Description = "Pearls HV", DisplayOrder = 17, Enabled = true },
                new ActivityType { Id = 18, Code = "PHONE CONTACT", Description = "Phone Contact w/Client", DisplayOrder = 18, Enabled = true },
                new ActivityType { Id = 19, Code = "PR", Description = "PR", DisplayOrder = 19, Enabled = true },
                new ActivityType { Id = 20, Code = "STAFFING", Description = "Clinical Supervision/Staffing", DisplayOrder = 20, Enabled = true },
                new ActivityType { Id = 21, Code = "SUP COUN", Description = "Supportive Counseling", DisplayOrder = 21, Enabled = true },
                new ActivityType { Id = 22, Code = "TRAVEL TIME", Description = "Travel Time", DisplayOrder = 22, Enabled = true }
            );
        }
    }
}