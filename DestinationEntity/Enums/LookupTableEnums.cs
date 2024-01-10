using System.ComponentModel.DataAnnotations;

namespace FSSRC_DataMigration.DestinationEntity.Enums
{
    public enum ServiceTypeEnum
    {
        [Display(Name = "HomeCare")] HomeCare = 1,
        [Display(Name = "Meals on Wheels")] MealsOnWheels = 2,
        [Display(Name = "Transportation")] Transportation = 3,
        [Display(Name = "Counseling and Advocacy")] CounselingAndAdvocacy = 4,
        [Display(Name = "Adult Protective Service")] AdultProtectiveService = 5
    }

    public enum SubServiceTypeEnum
    {
        Outreach = 1,
        Counseling = 2,
        Caregiver = 3,
        GRG = 4,
        SIS = 5,
        Options = 6
    }

    public enum ProgramTypeEnum
    {
        PEARLS = 1,
        [Display(Name = "Friendly Callers")]
        FriendlyCallers = 2,
        [Display(Name = "Activity Box")]
        ActivityBox = 3
    }

    public enum StatusEnum
    {
        Open = 1,
        Closed = 2,
        Hold = 3
    }
}
