using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class HomeCareRecord : BaseServiceRecord
    {
        public int? FundId { get; set; }
        public int? CaseManagerId { get; set; }
        public string? ServiceMax { get; set; }
        public int? Frequency { get; set; }
        public double? HoursRequired { get; set; }
        public double? ActualBillRate { get; set; }
        public double? StandardBillRate { get; set; }
        public string? AuthorizationNumber { get; set; }
        public DateTime? AuthorizationEndDate { get; set; }
        public DateTime? LastDateOfService { get; set; }
        public string? CaseManagerName { get; set; }
    }
    class HomeCareRecord_DestinationDb_Config : IEntityTypeConfiguration<HomeCareRecord>
    {
        public void Configure(EntityTypeBuilder<HomeCareRecord> entity)
        {
            entity.Ignore(x => x.CaseManagerName);
        }
    }
}
