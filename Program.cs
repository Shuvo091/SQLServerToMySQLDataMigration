using FSSRC_DataMigration.DestinationEntity;
using Microsoft.EntityFrameworkCore;

namespace FSSRC_DataMigration
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press 1 to reset the current database with SeniorResourceCenter database.");
            Console.WriteLine("Press 2 to add excel sheet's data to the current database.");
            Console.WriteLine("Press 3 to update case worker data on the current database.");
            Console.Write("Press: ");
            string input = Console.ReadLine();

            if (input == "1")
            {
                int counter = 1;
                Console.WriteLine("Data migration started...");

                // clean up data from destination
                cleanTables();

                // Client module
                DataMapping.Map<Client>("Client.sql", counter++);
                DataMapping.Map<ClientImpairment>("ClientImpairment.sql", counter++);
                DataMapping.Map<IncomeInformation>("IncomeInformation.sql", counter++);

                // Staff module
                DataMapping.Map<Staff>("Staff.sql", counter++);
                DataMapping.Map<StaffClassification>("StaffClassification.sql", counter++);
                DataMapping.Map<EmploymentHistory>("EmploymentHistory.sql", counter++);
                DataMapping.Map<InServiceInformation>("InserviceInformation.sql", counter++);

                //User module
                DataMapping.MapUserDataFromExcel("AspNetUsers.xlsx", counter++);
                counter++; //User claims is inserted

                //Case manager
                DataMapping.Map<CaseManager>("CaseManager.sql", counter++);

                //Emergency Contacts
                DataMapping.Map<EmergencyContact>("EmergencyContact.sql", counter++);

                //HomeCare
                DataMapping.Map<HomeCareRecord>("HomeCareRecord.sql", counter++);
                DataMapping.Map<ScheduleEvent>("ScheduleEvent.sql", counter++);
                DataMapping.Map<ActualEvent>("ActualEvent.sql", counter++);
                DataMapping.Map<Expense>("Expense.sql", counter++);
                DataMapping.Map<CareTypeValue>("CareTypeValue.sql", counter++);

                //MOW Module
                DataMapping.Map<MealsOnWheelsRecord>("MealsOnWheelsRecord.sql", counter++);
                DataMapping.Map<RouteInfo>("RouteInformation.sql", counter++);

                //Transportation module
                DataMapping.Map<TransportationRecord>("TransportationRecord.sql", counter++);
                DataMapping.Map<TransportationActivity>("TransportationActivity.sql", counter++);

                // Counseling and Advocacy
                DataMapping.Map<CounselingRecord>("CounselingRecord.sql", counter++);
                DataMapping.Map<CounselingActivity>("CounselingActivity.sql", counter++);
                DataMapping.MapAssistedWithFromCounselingActivity("CounselingActivity.sql", counter++);

                //Adult Protective Service module
                DataMapping.Map<APSRecord>("APSRecord.sql", counter++);
                DataMapping.Map<APSActivity>("APSActivity.sql", counter++);

                //Case notes Module
                DataMapping.Map<CaseNote>("CaseNote.sql", counter++);

                //Status Histories Module
                DataMapping.Map<StatusHistory>("StatusHistory.sql", counter++);

                //Post import additional SQL operations
                DataMapping.PostImportAdditionalOperations();
            }
            else if (input == "2")
            {
                //Manual data insertions
                DataMapping.MapDataManuallyFromExcel("Becki All Converted 2021-11-18 .xlsx");
            }
            else if (input == "3")
            {
                //Update CaseWorker assignments
                DataMapping.UpdateCounselingCaseWorkerAssignment("CounselingCounseling.sql", "CounselingCaregiver.sql", "CounselingOutreach.sql");
                DataMapping.UpdateAPSCaseWorkerAssignment("APSSeniorProtectiveService.sql", "APSSelfNeglect.sql");
                Console.WriteLine("Completed update case worker data on the current database...");
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
            

            

        }

        private static void cleanTables()
        {
            Console.WriteLine("Cleaning destination tables...");
            using (var destinationDbContext = new DestinationDbContext())
            {
                var tableName = "";
                //Always put the latest table on top to bypass reference error while deleting.
                tableName = destinationDbContext.Model.GetEntityTypes().First(t => t.ClrType == typeof(StatusHistory)).GetTableName();
                destinationDbContext.Database.ExecuteSqlRaw("DELETE FROM " + tableName);

                tableName = destinationDbContext.Model.GetEntityTypes().First(t => t.ClrType == typeof(CaseNote)).GetTableName();
                destinationDbContext.Database.ExecuteSqlRaw("DELETE FROM " + tableName);

                tableName = destinationDbContext.Model.GetEntityTypes().First(t => t.ClrType == typeof(APSActivity)).GetTableName();
                destinationDbContext.Database.ExecuteSqlRaw("DELETE FROM " + tableName);

                tableName = destinationDbContext.Model.GetEntityTypes().First(t => t.ClrType == typeof(APSRecord)).GetTableName();
                destinationDbContext.Database.ExecuteSqlRaw("DELETE FROM " + tableName);

                tableName = destinationDbContext.Model.GetEntityTypes().First(t => t.ClrType == typeof(CounselingActivitiesAssistedWith)).GetTableName();
                destinationDbContext.Database.ExecuteSqlRaw("DELETE FROM " + tableName);

                tableName = destinationDbContext.Model.GetEntityTypes().First(t => t.ClrType == typeof(CounselingActivity)).GetTableName();
                destinationDbContext.Database.ExecuteSqlRaw("DELETE FROM " + tableName);

                tableName = destinationDbContext.Model.GetEntityTypes().First(t => t.ClrType == typeof(CounselingRecord)).GetTableName();
                destinationDbContext.Database.ExecuteSqlRaw("DELETE FROM " + tableName);

                tableName = destinationDbContext.Model.GetEntityTypes().First(t => t.ClrType == typeof(TransportationActivity)).GetTableName();
                destinationDbContext.Database.ExecuteSqlRaw("DELETE FROM " + tableName);

                tableName = destinationDbContext.Model.GetEntityTypes().First(t => t.ClrType == typeof(TransportationRecord)).GetTableName();
                destinationDbContext.Database.ExecuteSqlRaw("DELETE FROM " + tableName);

                tableName = destinationDbContext.Model.GetEntityTypes().First(t => t.ClrType == typeof(RouteInfo)).GetTableName();
                destinationDbContext.Database.ExecuteSqlRaw("DELETE FROM " + tableName);

                tableName = destinationDbContext.Model.GetEntityTypes().First(t => t.ClrType == typeof(MealsOnWheelsRecord)).GetTableName();
                destinationDbContext.Database.ExecuteSqlRaw("DELETE FROM " + tableName);

                tableName = destinationDbContext.Model.GetEntityTypes().First(t => t.ClrType == typeof(CareTypeValue)).GetTableName();
                destinationDbContext.Database.ExecuteSqlRaw("DELETE FROM " + tableName);

                tableName = destinationDbContext.Model.GetEntityTypes().First(t => t.ClrType == typeof(Expense)).GetTableName();
                destinationDbContext.Database.ExecuteSqlRaw("DELETE FROM " + tableName);

                tableName = destinationDbContext.Model.GetEntityTypes().First(t => t.ClrType == typeof(ActualEvent)).GetTableName();
                destinationDbContext.Database.ExecuteSqlRaw("DELETE FROM " + tableName);

                tableName = destinationDbContext.Model.GetEntityTypes().First(t => t.ClrType == typeof(ScheduleEvent)).GetTableName();
                destinationDbContext.Database.ExecuteSqlRaw("DELETE FROM " + tableName);

                tableName = destinationDbContext.Model.GetEntityTypes().First(t => t.ClrType == typeof(HomeCareRecord)).GetTableName();
                destinationDbContext.Database.ExecuteSqlRaw("DELETE FROM " + tableName);

                tableName = destinationDbContext.Model.GetEntityTypes().First(t => t.ClrType == typeof(EmergencyContact)).GetTableName();
                destinationDbContext.Database.ExecuteSqlRaw("DELETE FROM " + tableName);

                tableName = destinationDbContext.Model.GetEntityTypes().First(t => t.ClrType == typeof(CaseManager)).GetTableName();
                destinationDbContext.Database.ExecuteSqlRaw("DELETE FROM " + tableName);

                tableName = destinationDbContext.Model.GetEntityTypes().First(t => t.ClrType == typeof(AspNetUserClaim)).GetTableName();
                destinationDbContext.Database.ExecuteSqlRaw("DELETE FROM " + tableName + " WHERE UserId <> (SELECT Id FROM AspNetUsers WHERE UserName = 'admin')");

                tableName = destinationDbContext.Model.GetEntityTypes().First(t => t.ClrType == typeof(AppUser)).GetTableName();
                destinationDbContext.Database.ExecuteSqlRaw("DELETE FROM " + tableName + " WHERE UserName <> 'admin'");

                tableName = destinationDbContext.Model.GetEntityTypes().First(t => t.ClrType == typeof(InServiceInformation)).GetTableName();
                destinationDbContext.Database.ExecuteSqlRaw("DELETE FROM " + tableName);

                tableName = destinationDbContext.Model.GetEntityTypes().First(t => t.ClrType == typeof(EmploymentHistory)).GetTableName();
                destinationDbContext.Database.ExecuteSqlRaw("DELETE FROM " + tableName);

                tableName = destinationDbContext.Model.GetEntityTypes().First(t => t.ClrType == typeof(StaffClassification)).GetTableName();
                destinationDbContext.Database.ExecuteSqlRaw("DELETE FROM " + tableName);

                tableName = destinationDbContext.Model.GetEntityTypes().First(t => t.ClrType == typeof(Staff)).GetTableName();
                destinationDbContext.Database.ExecuteSqlRaw("DELETE FROM " + tableName);

                tableName = destinationDbContext.Model.GetEntityTypes().First(t => t.ClrType == typeof(IncomeInformation)).GetTableName();
                destinationDbContext.Database.ExecuteSqlRaw("DELETE FROM " + tableName);

                tableName = destinationDbContext.Model.GetEntityTypes().First(t => t.ClrType == typeof(ClientImpairment)).GetTableName();
                destinationDbContext.Database.ExecuteSqlRaw("DELETE FROM " + tableName);

                tableName = destinationDbContext.Model.GetEntityTypes().First(t => t.ClrType == typeof(Client)).GetTableName();
                destinationDbContext.Database.ExecuteSqlRaw("DELETE FROM " + tableName);
            }
        }
    }
}
