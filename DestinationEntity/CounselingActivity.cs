using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class CounselingActivity
    {
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public DateTime? Date { get; set; }
        public int? Minutes { get; set; }
        public string? Note { get; set; }
        public DateTime? EnteredOn { get; set; }


        public string? CounselorUserName { get; set; }
        public Guid? CounselorId { get; set; }
        public int? CounselingRecordId { get; set; }
        public int? ActivityTypeId { get; set; }
        public int? SubServiceId { get; set; }
        public int? ProgramInfoId { get; set; }
        public int? AssistedWithId { get; set; }
        public Guid? EnteredById { get; set; }
    }

    class CounselingActivity_SourceDb_Config : IEntityTypeConfiguration<CounselingActivity>
    {
        public void Configure(EntityTypeBuilder<CounselingActivity> entity)
        {
            entity.Ignore(x => x.CounselorId);
            entity.Ignore(x => x.EnteredById);
        }
    }

    class CounselingActivity_DestinationDb_Config : IEntityTypeConfiguration<CounselingActivity>
    {
        public void Configure(EntityTypeBuilder<CounselingActivity> entity)
        {
            entity.Ignore(x => x.CounselorUserName);
            entity.Ignore(x => x.AssistedWithId);
        }
    }
}
