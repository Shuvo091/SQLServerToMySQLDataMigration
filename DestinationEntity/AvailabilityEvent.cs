using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json.Serialization;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class AvailabilityEvent
    {
        public int Id { get; set; }
        public int? StaffId { get; set; }
        public int? FollowingID { get; set; }
        public string Guid { get; set; }
        public string Subject { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string StartTimezone { get; set; }
        public string EndTimezone { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public bool IsAllDay { get; set; }
        public int? RecurrenceID { get; set; }
        public string RecurrenceRule { get; set; }
        public string RecurrenceException { get; set; }
        public bool IsReadonly { get; set; }
        public bool IsBlock { get; set; }
        public bool IsUnavailable { get; set; }
        [JsonIgnore]
        public virtual Staff Staff { get; set; }
    }

    public class AvailabilityEventConfig : IEntityTypeConfiguration<AvailabilityEvent>
    {
        public void Configure(EntityTypeBuilder<AvailabilityEvent> entity)
        {
            entity.Property(x => x.StartTime).HasConversion(
                v => v,
                v => new DateTime(v.Ticks, DateTimeKind.Utc));

            entity.Property(x => x.EndTime).HasConversion(
                v => v,
                v => new DateTime(v.Ticks, DateTimeKind.Utc));
        }
    }
}
