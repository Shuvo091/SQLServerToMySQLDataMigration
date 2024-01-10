using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class CareService : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CareTypeId { get; set; }
    }

    class CareServiceConfig : IEntityTypeConfiguration<CareService>
    {
        public void Configure(EntityTypeBuilder<CareService> entity)
        {
            entity.HasData(
                new CareService { Id = 1, Name = "Eating", CareTypeId = 1, DisplayOrder = 1, Enabled = true },
                new CareService { Id = 2, Name = "Meals", CareTypeId = 1, DisplayOrder = 2, Enabled = true },
                new CareService { Id = 3, Name = "Bathing", CareTypeId = 1, DisplayOrder = 3, Enabled = true },
                new CareService { Id = 4, Name = "Dressing", CareTypeId = 1, DisplayOrder = 4, Enabled = true },
                new CareService { Id = 5, Name = "Grooming", CareTypeId = 1, DisplayOrder = 5, Enabled = true },
                new CareService { Id = 6, Name = "Transfer/Positioning", CareTypeId = 1, DisplayOrder = 6, Enabled = true },
                new CareService { Id = 7, Name = "Continence", CareTypeId = 1, DisplayOrder = 7, Enabled = true },
                new CareService { Id = 8, Name = "Telephoning", CareTypeId = 2, DisplayOrder = 8, Enabled = true },
                new CareService { Id = 9, Name = "Housekeeping", CareTypeId = 2, DisplayOrder = 9, Enabled = true },
                new CareService { Id = 10, Name = "Laundry", CareTypeId = 2, DisplayOrder = 10, Enabled = true },
                new CareService { Id = 11, Name = "Transport", CareTypeId = 2, DisplayOrder = 11, Enabled = true },
                new CareService { Id = 12, Name = "Medication", CareTypeId = 3, DisplayOrder = 12, Enabled = true },
                new CareService { Id = 13, Name = "Supervision", CareTypeId = 4, DisplayOrder = 13, Enabled = true }
            );
        }
    }
}