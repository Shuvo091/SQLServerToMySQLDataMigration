using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class State : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Client> BillingClients { get; set; }
        public virtual ICollection<Staff> Staffs { get; set; }
    }

    class StateConfig : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> entity)
        {
            entity.HasData(
                new State { Id = 1, Name = "AL", DisplayOrder = 1, Enabled = true },
                new State { Id = 2, Name = "AK", DisplayOrder = 2, Enabled = true },
                new State { Id = 3, Name = "AZ", DisplayOrder = 3, Enabled = true },
                new State { Id = 4, Name = "AR", DisplayOrder = 4, Enabled = true },
                new State { Id = 5, Name = "CA", DisplayOrder = 5, Enabled = true },
                new State { Id = 6, Name = "CO", DisplayOrder = 6, Enabled = true },
                new State { Id = 7, Name = "CT", DisplayOrder = 7, Enabled = true },
                new State { Id = 8, Name = "DE", DisplayOrder = 8, Enabled = true },
                new State { Id = 9, Name = "FL", DisplayOrder = 9, Enabled = true },
                new State { Id = 10, Name = "GA", DisplayOrder = 10, Enabled = true },
                new State { Id = 11, Name = "HI", DisplayOrder = 11, Enabled = true },
                new State { Id = 12, Name = "ID", DisplayOrder = 12, Enabled = true },
                new State { Id = 13, Name = "IL", DisplayOrder = 13, Enabled = true },
                new State { Id = 14, Name = "IN", DisplayOrder = 14, Enabled = true },
                new State { Id = 15, Name = "IA", DisplayOrder = 15, Enabled = true },
                new State { Id = 16, Name = "KS", DisplayOrder = 16, Enabled = true },
                new State { Id = 17, Name = "KY", DisplayOrder = 17, Enabled = true },
                new State { Id = 18, Name = "LA", DisplayOrder = 18, Enabled = true },
                new State { Id = 19, Name = "ME", DisplayOrder = 19, Enabled = true },
                new State { Id = 20, Name = "MD", DisplayOrder = 20, Enabled = true },
                new State { Id = 21, Name = "MA", DisplayOrder = 21, Enabled = true },
                new State { Id = 22, Name = "MI", DisplayOrder = 22, Enabled = true },
                new State { Id = 23, Name = "MN", DisplayOrder = 23, Enabled = true },
                new State { Id = 24, Name = "MS", DisplayOrder = 24, Enabled = true },
                new State { Id = 25, Name = "MO", DisplayOrder = 25, Enabled = true },
                new State { Id = 26, Name = "MT", DisplayOrder = 26, Enabled = true },
                new State { Id = 27, Name = "NE", DisplayOrder = 27, Enabled = true },
                new State { Id = 28, Name = "NV", DisplayOrder = 28, Enabled = true },
                new State { Id = 29, Name = "NH", DisplayOrder = 29, Enabled = true },
                new State { Id = 30, Name = "NJ", DisplayOrder = 30, Enabled = true },
                new State { Id = 31, Name = "NM", DisplayOrder = 31, Enabled = true },
                new State { Id = 32, Name = "NY", DisplayOrder = 32, Enabled = true },
                new State { Id = 33, Name = "NC", DisplayOrder = 33, Enabled = true },
                new State { Id = 34, Name = "ND", DisplayOrder = 34, Enabled = true },
                new State { Id = 35, Name = "OH", DisplayOrder = 35, Enabled = true },
                new State { Id = 36, Name = "OK", DisplayOrder = 36, Enabled = true },
                new State { Id = 37, Name = "OR", DisplayOrder = 37, Enabled = true },
                new State { Id = 38, Name = "PA", DisplayOrder = 38, Enabled = true },
                new State { Id = 39, Name = "RI", DisplayOrder = 39, Enabled = true },
                new State { Id = 40, Name = "SC", DisplayOrder = 40, Enabled = true },
                new State { Id = 41, Name = "SD", DisplayOrder = 41, Enabled = true },
                new State { Id = 42, Name = "TN", DisplayOrder = 42, Enabled = true },
                new State { Id = 43, Name = "TX", DisplayOrder = 43, Enabled = true },
                new State { Id = 44, Name = "UT", DisplayOrder = 44, Enabled = true },
                new State { Id = 45, Name = "VT", DisplayOrder = 45, Enabled = true },
                new State { Id = 46, Name = "VA", DisplayOrder = 46, Enabled = true },
                new State { Id = 47, Name = "WA", DisplayOrder = 47, Enabled = true },
                new State { Id = 48, Name = "WV", DisplayOrder = 48, Enabled = true },
                new State { Id = 49, Name = "WI", DisplayOrder = 49, Enabled = true },
                new State { Id = 50, Name = "WY", DisplayOrder = 50, Enabled = true }
            );
        }
    }
}