using FSSRC_DataMigration.DestinationEntity.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class Classification : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<StaffClassification> StaffClassifications { get; set; }
    }

    class ClassificationConfig : IEntityTypeConfiguration<Classification>
    {
        public void Configure(EntityTypeBuilder<Classification> entity)
        {
            entity.HasData(
                new Classification { Id = (int)StaffClassificationEnum.ChurchWomenUnited, Name = "Church Women United", DisplayOrder = 1, Enabled = true },
                new Classification { Id = (int)StaffClassificationEnum.CouncilOfCongregations, Name = "Council of Congregations", DisplayOrder = 2, Enabled = true },
                new Classification { Id = (int)StaffClassificationEnum.HomeCareAssistant, Name = "Home Care Assistant", DisplayOrder = 3, Enabled = true },
                new Classification { Id = (int)StaffClassificationEnum.RetiredSeniorVolunteer, Name = "Retired Senior Volunteer", DisplayOrder = 4, Enabled = true },
                new Classification { Id = (int)StaffClassificationEnum.VolunteerGeneral, Name = "Volunteer General", DisplayOrder = 5, Enabled = true },
                new Classification { Id = (int)StaffClassificationEnum.VolunteerMOW, Name = "Volunteer MOW", DisplayOrder = 6, Enabled = true },
                new Classification { Id = (int)StaffClassificationEnum.VolunteerReassuranceCalling, Name = "Volunteer Reassurance Calling", DisplayOrder = 7, Enabled = true },
                new Classification { Id = (int)StaffClassificationEnum.VolunteerTransport, Name = "Volunteer Transport", DisplayOrder = 8, Enabled = true }
            );
        }
    }
}

