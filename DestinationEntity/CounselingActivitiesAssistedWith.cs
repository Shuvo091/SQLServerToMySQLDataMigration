using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class CounselingActivitiesAssistedWith
    {
        public int Id { get; set; }
        public int CounselingActivityId { get; set; }
        public int AssistedWithId { get; set; }
    }
}
