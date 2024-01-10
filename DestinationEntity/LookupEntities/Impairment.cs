using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class Impairment : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<ClientImpairment> ClientImpairments { get; set; } = new List<ClientImpairment>();
    }

    class ImpairmentConfig : IEntityTypeConfiguration<Impairment>
    {
        public void Configure(EntityTypeBuilder<Impairment> entity)
        {
            entity.HasData(
                new Impairment { Id = 1, Name = "﻿Arthritis", Enabled = true, DisplayOrder = 1 },
                new Impairment { Id = 2, Name = "Asthma", Enabled = true, DisplayOrder = 2 },
                new Impairment { Id = 3, Name = "Bed Bound", Enabled = true, DisplayOrder = 3 },
                new Impairment { Id = 4, Name = "Dementia", Enabled = true, DisplayOrder = 4 },
                new Impairment { Id = 5, Name = "Depression", Enabled = true, DisplayOrder = 5 },
                new Impairment { Id = 6, Name = "Diabetes", Enabled = true, DisplayOrder = 6 },
                new Impairment { Id = 7, Name = "HBP", Enabled = true, DisplayOrder = 7 },
                new Impairment { Id = 8, Name = "Hearing Impaired", Enabled = true, DisplayOrder = 8 },
                new Impairment { Id = 9, Name = "Heart Problems", Enabled = true, DisplayOrder = 9 },
                new Impairment { Id = 10, Name = "HoH", Enabled = true, DisplayOrder = 10 },
                new Impairment { Id = 11, Name = "Needs Oxygen", Enabled = true, DisplayOrder = 11 },
                new Impairment { Id = 12, Name = "Poor Ambulation", Enabled = true, DisplayOrder = 12 },
                new Impairment { Id = 13, Name = "Stroke", Enabled = true, DisplayOrder = 13 },
                new Impairment { Id = 14, Name = "Vision Impaired", Enabled = true, DisplayOrder = 14 },
                new Impairment { Id = 15, Name = "Visually impaired or Blind", Enabled = true, DisplayOrder = 15 },
                new Impairment { Id = 16, Name = "Walker / Cane", Enabled = true, DisplayOrder = 16 },
                new Impairment { Id = 17, Name = "Weak or Frail", Enabled = true, DisplayOrder = 17 },
                new Impairment { Id = 18, Name = "Wheel Chair Bound", Enabled = true, DisplayOrder = 18 }
            );
        }
    }
}