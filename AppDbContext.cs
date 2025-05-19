using FSSRC_DataMigration.DestinationEntity;
using Microsoft.EntityFrameworkCore;

namespace FSSRC_DataMigration
{
    public class CommonDbContext : DbContext
    {
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AppUser> AspNetUsers { get; set; }
        public virtual DbSet<IncomeInformation> IncomeInformation { get; set; }
        public virtual DbSet<ClientImpairment> ClientImpairments { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<EmploymentHistory> EmploymentHistories { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<StaffClassification> StaffClassifications { get; set; }
        public virtual DbSet<InServiceInformation> InserviceInformation { get; set; }
        public virtual DbSet<CaseManager> CaseManagers { get; set; }
        public virtual DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public virtual DbSet<HomeCareRecord> HomeCareRecords { get; set; }
        public virtual DbSet<ScheduleEvent> ScheduleEvents { get; set; }
        public virtual DbSet<ActualEvent> ActualEvents { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<CareTypeValue> CareTypeValues { get; set; }
        public virtual DbSet<MealsOnWheelsRecord> MealsOnWheelsRecords { get; set; }
        public virtual DbSet<RouteInfo> RouteInformation { get; set; }
        public virtual DbSet<TransportationRecord> TransportationRecords { get; set; }
        public virtual DbSet<TransportationActivity> TransportationActivities { get; set; }
        public virtual DbSet<CounselingRecord> CounselingRecords { get; set; }
        public virtual DbSet<CounselingActivity> CounselingActivities { get; set; }
        public virtual DbSet<CounselingActivitiesAssistedWith> CounselingActivitiesAssistedWith { get; set; }
        public virtual DbSet<APSRecord> APSRecords { get; set; }
        public virtual DbSet<APSActivity> APSActivities { get; set; }
        public virtual DbSet<CaseNote> CaseNotes { get; set; }
        public virtual DbSet<StatusHistory> StatusHistories { get; set; }
    }

    public class DestinationDbContext : CommonDbContext
    {
        public virtual DbSet<SIS> SIS { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var serverVersion = new MySqlServerVersion("8.0.26");
            optionsBuilder.UseMySql("CONNECTIONSTRING1", serverVersion);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CounselingRecord_DestinationDb_Config());
            modelBuilder.ApplyConfiguration(new CounselingActivity_DestinationDb_Config());
            modelBuilder.ApplyConfiguration(new APSRecord_DestinationDb_Config());
            modelBuilder.ApplyConfiguration(new APSActivity_DestinationDb_Config());
            modelBuilder.ApplyConfiguration(new MealsOnWheelsRecord_DestinationDb_Config());
            modelBuilder.ApplyConfiguration(new TransportationRecord_DestinationDb_Config());
            modelBuilder.ApplyConfiguration(new HomeCareRecord_DestinationDb_Config());
        }
    }

    public class SourceDbContext : CommonDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"CONNECTIONSTRING2",
                 sqlServerOptions => sqlServerOptions.CommandTimeout(600));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CounselingRecord_SourceDb_Config());
            modelBuilder.ApplyConfiguration(new CounselingActivity_SourceDb_Config());
            modelBuilder.ApplyConfiguration(new APSRecord_SourceDb_Config());
            modelBuilder.ApplyConfiguration(new APSActivity_SourceDb_Config());
            modelBuilder.ApplyConfiguration(new CaseNote_SourceDb_Config());
        }
    }
}
