using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class APSActivity
    {
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public DateTime? Date { get; set; }
        public int? Minutes { get; set; }
        public string? Note { get; set; }
        public DateTime? EnteredOn { get; set; }
        public int? ActivityTypeId { get; set; }
        public int? SISId { get; set; }
        public int? ServiceId { get; set; }


        public Guid? UserId { get; set; }
        public string? UserName { get; set; }
        public int ClientId { get; set; }
        public int APSRecordId { get; set; }
    }

    class APSActivity_SourceDb_Config : IEntityTypeConfiguration<APSActivity>
    {
        public void Configure(EntityTypeBuilder<APSActivity> entity)
        {
            entity.Ignore(x => x.UserId);
            entity.Ignore(x => x.APSRecordId);
        }
    }

    class APSActivity_DestinationDb_Config : IEntityTypeConfiguration<APSActivity>
    {
        public void Configure(EntityTypeBuilder<APSActivity> entity)
        {
            entity.Ignore(x => x.UserName);
            entity.Ignore(x => x.ClientId);
        }
    }
}
