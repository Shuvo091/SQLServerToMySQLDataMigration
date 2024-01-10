using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class ClientPresentingProblems
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int PresentingProblemsId { get; set; }
        public virtual Client Client { get; set; }
        public virtual PresentingProblems PresentingProblems { get; set; }

    }

    class ClientPresentingProblemsConfig : IEntityTypeConfiguration<ClientPresentingProblems>
    {
        public void Configure(EntityTypeBuilder<ClientPresentingProblems> entity)
        {
        }
    }
}