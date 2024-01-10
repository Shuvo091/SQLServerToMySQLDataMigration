using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class APSRecord : BaseServiceRecord
    {
        public Guid? CaseWorkerId { get; set; }
        public string? CaseWorkerUserName { get; set; }
    }

    class APSRecord_SourceDb_Config : IEntityTypeConfiguration<APSRecord>
    {
        public void Configure(EntityTypeBuilder<APSRecord> entity)
        {
            entity.Ignore(x => x.CaseWorkerId);
        }
    }

    class APSRecord_DestinationDb_Config : IEntityTypeConfiguration<APSRecord>
    {
        public void Configure(EntityTypeBuilder<APSRecord> entity)
        {
            entity.Ignore(x => x.CaseWorkerUserName);
        }
    }
}
