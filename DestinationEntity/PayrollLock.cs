using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    //This entity denotes a date and if that date is locked.
    //The date field is expressed as 'Number of days passed since 1/1/1970' and ranges from 1/1/1970 to 12/31/2099
    //Initially all the dates were set as IsLocked=false
    public class PayrollLock
    {
        public int Id { get; set; }
        public int DaysFrom1970 { get; set; }
        public bool IsLocked { get; set; }
    }
    class PayrollLockConfig : IEntityTypeConfiguration<PayrollLock>
    {
        public void Configure(EntityTypeBuilder<PayrollLock> entity)
        {
            List<PayrollLock> initData = new List<PayrollLock>();
            for (int i = 1; i <= 50000; i++)
            {
                initData.Add(new PayrollLock { Id = i, DaysFrom1970 = i, IsLocked = false });
            }
            entity.HasData(initData);
        }
    }
}
