using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class PresentingProblems : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ClientPresentingProblems> ClientPresentingProblems { get; set; }
    }

    class PresentingProblemsConfig : IEntityTypeConfiguration<PresentingProblems>
    {
        public void Configure(EntityTypeBuilder<PresentingProblems> entity)
        {
        }
    }
}

