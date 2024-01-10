using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class StatusReason : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
    }

    class StatusReasonConfig : IEntityTypeConfiguration<StatusReason>
    {
        public void Configure(EntityTypeBuilder<StatusReason> entity)
        {
            entity.HasData(
                new StatusReason { Id = 1, Name = "Referral - 211 Information Hotline", StatusId = 1, Enabled = true, DisplayOrder = 1 },
                new StatusReason { Id = 2, Name = "Referral - Anonymous", StatusId = 1, Enabled = true, DisplayOrder = 2 },
                new StatusReason { Id = 3, Name = "Referral - Assisted Living", StatusId = 1, Enabled = true, DisplayOrder = 3 },
                new StatusReason { Id = 4, Name = "Referral - Carle MCCD", StatusId = 1, Enabled = true, DisplayOrder = 4 },
                new StatusReason { Id = 5, Name = "Referral - CCU -  Client Requested SRC", StatusId = 1, Enabled = true, DisplayOrder = 5 },
                new StatusReason { Id = 6, Name = "Referral - CCU - Assigned By CCU", StatusId = 1, Enabled = true, DisplayOrder = 6 },
                new StatusReason { Id = 7, Name = "Referral - Client Before", StatusId = 1, Enabled = true, DisplayOrder = 7 },
                new StatusReason { Id = 8, Name = "Referral - Cumberland Case Coordination Unit", StatusId = 1, Enabled = true, DisplayOrder = 8 },
                new StatusReason { Id = 9, Name = "Referral - Discharge Social Services- Carle", StatusId = 1, Enabled = true, DisplayOrder = 9 },
                new StatusReason { Id = 10, Name = "Referral - Discharge Social Services- Provena", StatusId = 1, Enabled = true, DisplayOrder = 10 },
                new StatusReason { Id = 11, Name = "Referral - Doctor's Office", StatusId = 1, Enabled = true, DisplayOrder = 11 },
                new StatusReason { Id = 12, Name = "Referral - Elder Abuse Report", StatusId = 1, Enabled = true, DisplayOrder = 12 },
                new StatusReason { Id = 13, Name = "Referral - Family or Friend", StatusId = 1, Enabled = true, DisplayOrder = 13 },
                new StatusReason { Id = 14, Name = "Referral - Home Health- Carle", StatusId = 1, Enabled = true, DisplayOrder = 14 },
                new StatusReason { Id = 15, Name = "Referral - Home Health- Other", StatusId = 1, Enabled = true, DisplayOrder = 15 },
                new StatusReason { Id = 16, Name = "Referral - Home Health- Provena", StatusId = 1, Enabled = true, DisplayOrder = 16 },
                new StatusReason { Id = 17, Name = "Referral - IDHS", StatusId = 1, Enabled = true, DisplayOrder = 17 },
                new StatusReason { Id = 18, Name = "Referral - IDOA", StatusId = 1, Enabled = true, DisplayOrder = 18 },
                new StatusReason { Id = 19, Name = "Referral - Media", StatusId = 1, Enabled = true, DisplayOrder = 19 },
                new StatusReason { Id = 20, Name = "Referral - Mental Health Center", StatusId = 1, Enabled = true, DisplayOrder = 20 },
                new StatusReason { Id = 21, Name = "Referral - Nursing Home", StatusId = 1, Enabled = true, DisplayOrder = 21 },
                new StatusReason { Id = 22, Name = "Referral - ORS", StatusId = 1, Enabled = true, DisplayOrder = 22 },
                new StatusReason { Id = 23, Name = "Referral - Other", StatusId = 1, Enabled = true, DisplayOrder = 23 },
                new StatusReason { Id = 24, Name = "Referral - Police/Fire", StatusId = 1, Enabled = true, DisplayOrder = 24 },
                new StatusReason { Id = 25, Name = "Referral - PR Event", StatusId = 1, Enabled = true, DisplayOrder = 25 },
                new StatusReason { Id = 26, Name = "Referral - Public Health", StatusId = 1, Enabled = true, DisplayOrder = 26 },
                new StatusReason { Id = 27, Name = "Referral - Senior Housing", StatusId = 1, Enabled = true, DisplayOrder = 27 },
                new StatusReason { Id = 28, Name = "Referral - Social Service Agency", StatusId = 1, Enabled = true, DisplayOrder = 28 },
                new StatusReason { Id = 29, Name = "Referral - SRC Program", StatusId = 1, Enabled = true, DisplayOrder = 29 },
                new StatusReason { Id = 30, Name = "Referral - Veteran's Administration", StatusId = 1, Enabled = true, DisplayOrder = 30 },
                new StatusReason { Id = 31, Name = "Referral - Web Page", StatusId = 1, Enabled = true, DisplayOrder = 31 },
                new StatusReason { Id = 32, Name = "Referral - Yellow Pages", StatusId = 1, Enabled = true, DisplayOrder = 32 },
                new StatusReason { Id = 33, Name = "Termination Reason - Agency Initiated", StatusId = 2, Enabled = true, DisplayOrder = 1 },
                new StatusReason { Id = 34, Name = "Termination Reason - ANE Administrative Closure", StatusId = 2, Enabled = true, DisplayOrder = 2 },
                new StatusReason { Id = 35, Name = "Termination Reason - ANE Unsubstantiated", StatusId = 2, Enabled = true, DisplayOrder = 3 },
                new StatusReason { Id = 36, Name = "Termination Reason - Deceased", StatusId = 2, Enabled = true, DisplayOrder = 4 },
                new StatusReason { Id = 37, Name = "Termination Reason - Dissatisfied with Service (Financial)", StatusId = 2, Enabled = true, DisplayOrder = 5 },
                new StatusReason { Id = 38, Name = "Termination Reason - Dissatisfied with Service (Quality)", StatusId = 2, Enabled = true, DisplayOrder = 6 },
                new StatusReason { Id = 39, Name = "Termination Reason - Dissatisfied with Service (Schedule)", StatusId = 2, Enabled = true, DisplayOrder = 7 },
                new StatusReason { Id = 40, Name = "Termination Reason - Moved Out of Area", StatusId = 2, Enabled = true, DisplayOrder = 8 },
                new StatusReason { Id = 41, Name = "Termination Reason - Moved to Assistive Setting", StatusId = 2, Enabled = true, DisplayOrder = 9 },
                new StatusReason { Id = 42, Name = "Termination Reason - Needs Met", StatusId = 2, Enabled = true, DisplayOrder = 10 },
                new StatusReason { Id = 43, Name = "Termination Reason - Nursing Home", StatusId = 2, Enabled = true, DisplayOrder = 11 },
                new StatusReason { Id = 44, Name = "Termination Reason - Nursing Home Rehabilitation", StatusId = 2, Enabled = true, DisplayOrder = 12 },
                new StatusReason { Id = 45, Name = "Termination Reason - Refused Service", StatusId = 2, Enabled = true, DisplayOrder = 13 },
                new StatusReason { Id = 46, Name = "Termination Reason - Rehab Client  (Resumed Independence)", StatusId = 2, Enabled = true, DisplayOrder = 14 },
                new StatusReason { Id = 47, Name = "Termination Reason - Service Not Started", StatusId = 2, Enabled = true, DisplayOrder = 15 },
                new StatusReason { Id = 48, Name = "Termination Reason - Transferred to another SRC program", StatusId = 2, Enabled = true, DisplayOrder = 16 },
                new StatusReason { Id = 49, Name = "Termination Reason - Vendor Transfer (CCP) (Financial)", StatusId = 2, Enabled = true, DisplayOrder = 17 },
                new StatusReason { Id = 50, Name = "Termination Reason - Vendor Transfer (CCP) (Quality)", StatusId = 2, Enabled = true, DisplayOrder = 18 },
                new StatusReason { Id = 51, Name = "Termination Reason - Vendor Transfer (CCP) (Schedule)", StatusId = 2, Enabled = true, DisplayOrder = 19 },
                new StatusReason { Id = 52, Name = "Hold", StatusId = 3, Enabled = true, DisplayOrder = 1 }
            );
        }
    }
}