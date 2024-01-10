using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class TransportationRecord : BaseServiceRecord
    {
        public int? CaseManagerId { get; set; }
        public string? CaseManagerName { get; set; }
    }
    class TransportationRecord_DestinationDb_Config : IEntityTypeConfiguration<TransportationRecord>
    {
        public void Configure(EntityTypeBuilder<TransportationRecord> entity)
        {
            entity.Ignore(x => x.CaseManagerName);
        }
    }
}
