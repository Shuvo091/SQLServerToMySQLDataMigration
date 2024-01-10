using FSSRC_DataMigration.DestinationEntity.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class CaseNote
    {
        public int Id { get; set; }
        public int? StaffId { get; set; }
        public int? HomeCareRecordId { get; set; }
        public int? MealsOnWheelsRecordId { get; set; }
        public int? TransportationRecordId { get; set; }
        public bool Enabled { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Time { get; set; }
        public int? InfoViaId { get; set; }
        public ToOrFrom? ToOrFrom { get; set; }
        public int? SourceId { get; set; }
        public string? Note { get; set; }
        public Guid? EnteredById { get; set; }
        public DateTime? EnteredOn { get; set; }
        public Guid? UserId { get; set; }
    }

    class CaseNote_SourceDb_Config : IEntityTypeConfiguration<CaseNote>
    {
        public void Configure(EntityTypeBuilder<CaseNote> entity)
        {
            entity.Ignore(x => x.UserId);
            entity.Ignore(x => x.EnteredById);
        }
    }
}