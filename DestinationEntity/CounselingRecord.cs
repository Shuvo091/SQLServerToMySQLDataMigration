using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class CounselingRecord : BaseServiceRecord
    {
        public string? CaseWorkerUserName { get; set; }
        public Guid? CaseWorkerId { get; set; }
        public string? Title20Directions { get; set; }
    }

    class CounselingRecord_SourceDb_Config : IEntityTypeConfiguration<CounselingRecord>
    {
        public void Configure(EntityTypeBuilder<CounselingRecord> entity)
        {
            entity.Ignore(x => x.CaseWorkerId);
        }
    }

    class CounselingRecord_DestinationDb_Config : IEntityTypeConfiguration<CounselingRecord>
    {
        public void Configure(EntityTypeBuilder<CounselingRecord> entity)
        {
            entity.Ignore(x => x.CaseWorkerUserName);
        }
    }
}
