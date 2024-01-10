using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class MealsOnWheelsRecord : BaseServiceRecord
    {
        public int? CaseManagerId { get; set; }
        public DateTime? MealsOpenDate { get; set; }
        public DateTime? MealsCloseDate { get; set; }
        public bool? UserBilltoAddress { get; set; }
        public bool? BillClientAsPrimary { get; set; }
        public string? Instructions { get; set; }
        public string? RouteDirections { get; set; }
        public string? CaseManagerName { get; set; }
    }

    class MealsOnWheelsRecord_DestinationDb_Config : IEntityTypeConfiguration<MealsOnWheelsRecord>
    {
        public void Configure(EntityTypeBuilder<MealsOnWheelsRecord> entity)
        {
            entity.Ignore(x => x.CaseManagerName);
        }
    }
}
