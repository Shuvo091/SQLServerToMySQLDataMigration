using FSSRC_DataMigration.DestinationEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace FSSRC_DataMigration
{
    public static class DataMapping
    {
        const string passwordHash = "PLACEHOLDERHASH";
        public static void Map<T>(string fileName, int counter) where T : class
        {
            var path = Path.Combine(AppContext.BaseDirectory, "SourceDataQuery", fileName);
            string query = File.ReadAllText(path);

            var result = new List<T>();
            using (var sdc = new SourceDbContext())
            {
                result = sdc.Set<T>()
                   .FromSqlRaw(query)
                   .AsNoTracking()
                   .ToList();
            }

            // Data manipulation before inserting(if required)
            switch (typeof(T).Name)
            {
                case nameof(Client):
                    Client_PostDataManipulation(result as List<Client>);
                    break;
                case nameof(InServiceInformation):
                    InserviceInfo_PostDataManipulation(result as List<InServiceInformation>);
                    break;
                case nameof(CaseManager):
                    CaseManager_PostDataManipulation(result as List<CaseManager>);
                    break;
                case nameof(HomeCareRecord):
                    HomeCareRecord_PostDataManipulation(result as List<HomeCareRecord>);
                    break;
                case nameof(ScheduleEvent):
                    ScheduleEvent_PostDataManipulation(result as List<ScheduleEvent>);
                    break;
                case nameof(ActualEvent):
                    ActualEvent_PostDataManipulation(result as List<ActualEvent>);
                    break;
                case nameof(Expense):
                    Expense_PostDataManipulation(result as List<Expense>);
                    break;
                case nameof(CareTypeValue):
                    CareTypeValue_PostDataManipulation(result as List<CareTypeValue>);
                    break;
                case nameof(MealsOnWheelsRecord):
                    MealsOnWheelsRecord_PostDataManipulation(result as List<MealsOnWheelsRecord>);
                    break;
                case nameof(RouteInfo):
                    RouteInformation_PostDataManipulation(result as List<RouteInfo>);
                    break;
                case nameof(TransportationRecord):
                    TransportationRecord_PostDataManipulation(result as List<TransportationRecord>);
                    break;
                case nameof(TransportationActivity):
                    TransportationActivity_PostDataManipulation(result as List<TransportationActivity>);
                    break;
                case nameof(CounselingRecord):
                    CounselingRecord_PostDataManipulation(result as List<CounselingRecord>);
                    break;
                case nameof(CounselingActivity):
                    CounselingActivity_PostDataManipulation(result as List<CounselingActivity>);
                    break;
                case nameof(APSRecord):
                    APSRecord_PostDataManipulation(result as List<APSRecord>);
                    break;
                case nameof(APSActivity):
                    APSActivity_PostDataManipulation(result as List<APSActivity>);
                    break;
                case nameof(CaseNote):
                    CaseNote_PostDataManipulation(result as List<CaseNote>);
                    break;
                case nameof(StatusHistory):
                    StatusHistory_PostDataManipulation(result as List<StatusHistory>);
                    break;
            }

            result = ConvertDateTimeToUTC(result);

            using (var destinationDbContext = new DestinationDbContext())
            {
                // Insert all records
                destinationDbContext.Set<T>().AddRange(result);
                destinationDbContext.SaveChanges();
            }

            Console.WriteLine($"{counter} of 27 completed...");
        }

        public static List<T> ConvertDateTimeToUTC<T>(List<T> inputList)
        {
            var resultList = new List<T>();
            foreach (var item in inputList)
            {
                var itemType = item.GetType();
                var properties = itemType.GetProperties();
                var newItem = Activator.CreateInstance(itemType);

                foreach (var property in properties)
                {
                    if (property.PropertyType == typeof(DateTime))
                    {
                        var originalValue = (DateTime)property.GetValue(item);
                        double offsetInHours = TimeZoneInfo.FindSystemTimeZoneById("America/Chicago").GetUtcOffset(originalValue).TotalHours;
                        property.SetValue(newItem, originalValue.AddHours(-offsetInHours));
                    }
                    else if (property.PropertyType == typeof(DateTime?))
                    {
                        var nullableValue = property.GetValue(item) as DateTime?;
                        if (nullableValue.HasValue)
                        {
                            double offsetInHours = TimeZoneInfo.FindSystemTimeZoneById("America/Chicago").GetUtcOffset(nullableValue.Value).TotalHours;
                            property.SetValue(newItem, nullableValue.Value.AddHours(-offsetInHours));
                        }
                    }
                    else
                    {
                        property.SetValue(newItem, property.GetValue(item));
                    }
                }

                resultList.Add((T)newItem);
            }

            return resultList;
        }

        #region Update Counseling and APS caseworker assignments
        public static void UpdateAPSCaseWorkerAssignment(string SPS_fileName, string SN_fileName)
        {
            var SPS_path = Path.Combine(AppContext.BaseDirectory, "SourceDataQuery", SPS_fileName);
            var SN_path = Path.Combine(AppContext.BaseDirectory, "SourceDataQuery", SN_fileName);
            string SPS_query = File.ReadAllText(SPS_path);
            string SN_query = File.ReadAllText(SN_path);

            var SPS_result = new List<APSRecord>();
            var SN_result = new List<APSRecord>();
            using (var sdc = new SourceDbContext())
            {
                SPS_result = sdc.APSRecords
                   .FromSqlRaw(SPS_query)
                   .AsNoTracking()
                   .ToList();
                SN_result = sdc.APSRecords
                   .FromSqlRaw(SN_query)
                   .AsNoTracking()
                   .ToList();
            }
            
            using (var destinationDbContext = new DestinationDbContext())
            {
                // Update all records
                var apsRecords = destinationDbContext.APSRecords.ToList();
                var users = destinationDbContext.AspNetUsers.ToList();
                foreach (var apsRecord in apsRecords)
                {
                    var SPSRecord = SPS_result.Where(x => x.ClientId == apsRecord.ClientId).FirstOrDefault();
                    var SNRecord = SN_result.Where(x => x.ClientId == apsRecord.ClientId).FirstOrDefault();
                    if(SPSRecord != null && SPSRecord.CaseWorkerUserName != null)
                    {
                        //Set aps record caseworkerId to SNS
                        apsRecord.CaseWorkerId = users.Where(x => x.UserName == SPSRecord.CaseWorkerUserName).Select(x => x.Id).FirstOrDefault();
                        apsRecord.CaseWorkerId = apsRecord.CaseWorkerId == Guid.Empty ? null : apsRecord.CaseWorkerId;
                    }
                    else if(SNRecord != null && SNRecord.CaseWorkerUserName != null)
                    {
                        //Set aps record caseworkerId to SF
                        apsRecord.CaseWorkerId = users.Where(x => x.UserName == SNRecord.CaseWorkerUserName).Select(x => x.Id).FirstOrDefault();
                        apsRecord.CaseWorkerId = apsRecord.CaseWorkerId == Guid.Empty ? null : apsRecord.CaseWorkerId;
                    }
                    else
                    {
                        apsRecord.CaseWorkerId = null;
                    }
                }
                destinationDbContext.APSRecords.UpdateRange(apsRecords);
                destinationDbContext.SaveChanges();
            }
        }
        public static void UpdateCounselingCaseWorkerAssignment(string counseling_fileName, string caregiver_fileName, string outreach_fileName)
        {
            var counseling_path = Path.Combine(AppContext.BaseDirectory, "SourceDataQuery", counseling_fileName);
            var caregiver_path = Path.Combine(AppContext.BaseDirectory, "SourceDataQuery", caregiver_fileName);
            var outreach_path = Path.Combine(AppContext.BaseDirectory, "SourceDataQuery", outreach_fileName);
            string counseling_query = File.ReadAllText(counseling_path);
            string caregiver_query = File.ReadAllText(caregiver_path);
            string outreach_query = File.ReadAllText(outreach_path);

            var counseling_result = new List<CounselingRecord>();
            var caregiver_result = new List<CounselingRecord>();
            var outreach_result = new List<CounselingRecord>();
            using (var sdc = new SourceDbContext())
            {
                counseling_result = sdc.CounselingRecords
                   .FromSqlRaw(counseling_query)
                   .AsNoTracking()
                   .ToList();
                caregiver_result = sdc.CounselingRecords
                   .FromSqlRaw(caregiver_query)
                   .AsNoTracking()
                   .ToList();
                outreach_result = sdc.CounselingRecords
                   .FromSqlRaw(outreach_query)
                   .AsNoTracking()
                   .ToList();
            }

            using (var destinationDbContext = new DestinationDbContext())
            {
                // Update all records
                var cRecords = destinationDbContext.CounselingRecords.ToList();
                var users = destinationDbContext.AspNetUsers.ToList();
                foreach (var cRecord in cRecords)
                {
                    var counselingRecord = counseling_result.Where(x => x.ClientId == cRecord.ClientId).FirstOrDefault();
                    var caregiverRecord = caregiver_result.Where(x => x.ClientId == cRecord.ClientId).FirstOrDefault();
                    var outreachRecord = outreach_result.Where(x => x.ClientId == cRecord.ClientId).FirstOrDefault();
                    if (counselingRecord != null && counselingRecord.CaseWorkerUserName != null)
                    {
                        //Set counseling record caseworkerId to SNS
                        cRecord.CaseWorkerId = users.Where(x => x.UserName == counselingRecord.CaseWorkerUserName).Select(x => x.Id).FirstOrDefault();
                        cRecord.CaseWorkerId = cRecord.CaseWorkerId == Guid.Empty ? null : cRecord.CaseWorkerId;
                    }
                    else if (caregiverRecord != null && caregiverRecord.CaseWorkerUserName != null)
                    {
                        //Set caregiver record caseworkerId to SF
                        cRecord.CaseWorkerId = users.Where(x => x.UserName == caregiverRecord.CaseWorkerUserName).Select(x => x.Id).FirstOrDefault();
                        cRecord.CaseWorkerId = cRecord.CaseWorkerId == Guid.Empty ? null : cRecord.CaseWorkerId;
                    }
                    else if (outreachRecord != null && outreachRecord.CaseWorkerUserName != null)
                    {
                        //Set outreach record caseworkerId to SF
                        cRecord.CaseWorkerId = users.Where(x => x.UserName == outreachRecord.CaseWorkerUserName).Select(x => x.Id).FirstOrDefault();
                        cRecord.CaseWorkerId = cRecord.CaseWorkerId == Guid.Empty ? null : cRecord.CaseWorkerId;
                    }
                    else
                    {
                        cRecord.CaseWorkerId = null;
                    }
                }
                destinationDbContext.CounselingRecords.UpdateRange(cRecords);
                destinationDbContext.SaveChanges();
            }
        }
        #endregion

        #region Map from Excel
        public static void MapUserDataFromExcel(string fileName, int counter)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "SourceDataQuery", fileName);
            List<AppUser> entities = new List<AppUser>();
            List<AspNetUserClaim> claims = new List<AspNetUserClaim>();

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // Use NPOI to read the Excel file
                // Create a workbook object from the stream
                XSSFWorkbook workbook = new XSSFWorkbook(stream);
                // Get the first sheet from the workbook
                ISheet sheet = workbook.GetSheetAt(0);

                // Iterate through each row
                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);
                    if (row == null) continue;

                    // Create a new entity
                    var entity = new AppUser
                    {
                        // Map data from the data row to the entity
                        Id = Guid.NewGuid(),
                        UserName = row.GetCell(1).ToString().Trim(),
                        NormalizedUserName = row.GetCell(2).ToString().Trim(),
                        Email = row.GetCell(3).ToString().Trim(),
                        NormalizedEmail = row.GetCell(4).ToString().Trim(),
                        EmailConfirmed = row.GetCell(5).ToString().Trim() == "0" ? false : true,
                        PasswordHash = passwordHash,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        ConcurrencyStamp = row.GetCell(8).ToString().Trim(),
                        PhoneNumber = row.GetCell(9)?.ToString(),
                        PhoneNumberConfirmed = row.GetCell(10).ToString().Trim() == "0" ? false : true,
                        TwoFactorEnabled = row.GetCell(11).ToString().Trim() == "0" ? false : true,
                        LockoutEnd = null,
                        LockoutEnabled = row.GetCell(13).ToString().Trim() == "0" ? false : true,
                        AccessFailedCount = Convert.ToInt32(row.GetCell(14).ToString().Trim()),
                        FirstName = row.GetCell(15)?.ToString().Trim(),
                        LastName = row.GetCell(16)?.ToString().Trim(),
                        IsDeleted = row.GetCell(17)?.ToString().Trim() == "0" ? false : true,
                    };

                    // Create claims 
                    var claim = new AspNetUserClaim
                    {
                        Id = i + 1,
                        UserId = entity.Id,
                        ClaimType = "AccessPermission",
                        ClaimValue = row.GetCell(18)?.ToString().Trim()
                    };
                    // Add the entity to the list
                    entities.Add(entity);
                    claims.Add(claim);
                }
                using (var destinationDbContext = new DestinationDbContext())
                {
                    // Insert all records
                    destinationDbContext.AspNetUsers.AddRange(entities);
                    destinationDbContext.SaveChanges();
                    Console.WriteLine($"{counter++} of 27 completed...");
                    destinationDbContext.AspNetUserClaims.AddRange(claims);
                    destinationDbContext.SaveChanges();
                }
                Console.WriteLine($"{counter} of 27 completed...");
            }
        }
        public static void MapClientDataFromExcel(ISheet sheet)
        {
            List<Client> entities = new List<Client>();
            List<EmergencyContact> emergencyContactEntities = new List<EmergencyContact>();
            // Iterate through each row
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                if (row == null) continue;

                var entity = new Client();
                var emergencyContactEntity = new EmergencyContact();
                entity.Id = StringToInt(GetActualCellValue(row.GetCell((int)ClientFieldIndex.Id))).Value;
                entity.FirstName = GetActualCellValue(row.GetCell((int)ClientFieldIndex.FirstName));
                entity.LastName = GetActualCellValue(row.GetCell((int)ClientFieldIndex.LastName));
                entity.MI = GetActualCellValue(row.GetCell((int)ClientFieldIndex.MI));
                entity.PreferredName = GetActualCellValue(row.GetCell((int)ClientFieldIndex.PreferredName));
                entity.Tips = GetActualCellValue(row.GetCell((int)ClientFieldIndex.Tips));
                entity.HomeAddress = GetActualCellValue(row.GetCell((int)ClientFieldIndex.HomeAddress));
                entity.City = GetActualCellValue(row.GetCell((int)ClientFieldIndex.City));
                entity.StateId = StringToInt(GetActualCellValue(row.GetCell((int)ClientFieldIndex.StateId)));
                entity.ZipCode = GetActualCellValue(row.GetCell((int)ClientFieldIndex.ZipCode));
                entity.CountyId = StringToInt(GetActualCellValue(row.GetCell((int)ClientFieldIndex.CountyId)));
                entity.Email = GetActualCellValue(row.GetCell((int)ClientFieldIndex.Email));
                entity.Birthdate = StringToDateTime(GetActualCellValue(row.GetCell((int)ClientFieldIndex.Birthdate)));
                entity.HomePhone = GetActualCellValue(row.GetCell((int)ClientFieldIndex.HomePhone));
                entity.OtherPhone = GetActualCellValue(row.GetCell((int)ClientFieldIndex.OtherPhone));
                entity.OtherPhoneDesc = GetActualCellValue(row.GetCell((int)ClientFieldIndex.OtherPhoneDesc));
                entity.BillingName = GetActualCellValue(row.GetCell((int)ClientFieldIndex.BillingName));
                entity.BillingAddress = GetActualCellValue(row.GetCell((int)ClientFieldIndex.BillingAddress));
                entity.BillingCity = GetActualCellValue(row.GetCell((int)ClientFieldIndex.BillingCity));
                entity.BillingStateId = StringToInt(GetActualCellValue(row.GetCell((int)ClientFieldIndex.BillingStateId)));
                entity.SocialSecurity = GetActualCellValue(row.GetCell((int)ClientFieldIndex.SocialSecurity));
                entity.Medicaid = GetActualCellValue(row.GetCell((int)ClientFieldIndex.Medicaid));
                entity.InsuranceId = GetActualCellValue(row.GetCell((int)ClientFieldIndex.InsuranceId));
                entity.DASHPass = StringToBool(GetActualCellValue(row.GetCell((int)ClientFieldIndex.DASHPass)));
                entity.GenderId = StringToInt(GetActualCellValue(row.GetCell((int)ClientFieldIndex.GenderId)));
                entity.RaceId = StringToInt(GetActualCellValue(row.GetCell((int)ClientFieldIndex.RaceId)));
                entity.EthnicityId = StringToInt(GetActualCellValue(row.GetCell((int)ClientFieldIndex.EthnicityId)));
                entity.MaritalStatusId = StringToInt(GetActualCellValue(row.GetCell((int)ClientFieldIndex.MaritalStatusId)));
                entity.LivingStatusId = StringToInt(GetActualCellValue(row.GetCell((int)ClientFieldIndex.LivingStatusId)));
                entity.LivingArrangementId = StringToInt(GetActualCellValue(row.GetCell((int)ClientFieldIndex.LivingArrangementId)));
                entity.PetId = StringToInt(GetActualCellValue(row.GetCell((int)ClientFieldIndex.PetId)));
                entity.DistrictId = StringToInt(GetActualCellValue(row.GetCell((int)ClientFieldIndex.DistrictId)));
                entity.PovertyLevelId = StringToInt(GetActualCellValue(row.GetCell((int)ClientFieldIndex.PovertyLevelId)));
                entity.HouseholdIncome = StringToDouble(GetActualCellValue(row.GetCell((int)ClientFieldIndex.HouseholdIncome)));
                entity.BillingZip = GetActualCellValue(row.GetCell((int)ClientFieldIndex.BillingZip));
                entity.IntakeDate = StringToDateTime(GetActualCellValue(row.GetCell((int)ClientFieldIndex.IntakeDate)));
                entity.IntakeStaff = GetActualCellValue(row.GetCell((int)ClientFieldIndex.IntakeStaff));
                entity.ReferredById = StringToInt(GetActualCellValue(row.GetCell((int)ClientFieldIndex.ReferredById)));
                entity.IfRefferedByOther = GetActualCellValue(row.GetCell((int)ClientFieldIndex.IfRefferedByOther));
                entity.IntakeNotes = null;

                emergencyContactEntity.Notes = GetActualCellValue(row.GetCell((int)ClientFieldIndex.EmergencyContactDesc));
                emergencyContactEntity.ClientId = entity.Id;
                emergencyContactEntity.RelationshipId = 7; //Relationship -> other
                emergencyContactEntity.Enabled = true;
                if(emergencyContactEntity.Notes != null) emergencyContactEntities.Add(emergencyContactEntity);
                entities.Add(entity);
            }
            entities = ConvertDateTimeToUTC(entities);
            using (var destinationDbContext = new DestinationDbContext())
            {
                // Insert all records
                destinationDbContext.Clients.AddRange(entities);
                destinationDbContext.SaveChanges();
                destinationDbContext.EmergencyContacts.AddRange(emergencyContactEntities);
                destinationDbContext.SaveChanges();
            }
            Console.WriteLine("Clients from excel insertion completed...");
        }
        public static List<CounselingRecord> MapCounselingRecordDataFromExcel(ISheet sheet)
        {
            // Insert all records
            List<CounselingRecord> entities = new List<CounselingRecord>();
            using (var destinationDbContext = new DestinationDbContext())
            {

                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);
                    if (row == null) continue;

                    var entity = new CounselingRecord();
                    entity.Id = StringToInt(GetActualCellValue(row.GetCell((int)CounselingRecordFieldIndex.Id))).Value;
                    entity.CaseWorkerId = GetCaseWorkerId(GetActualCellValue(row.GetCell((int)CounselingRecordFieldIndex.CaseWorkerId)));
                    entity.Title20Directions = GetActualCellValue(row.GetCell((int)CounselingRecordFieldIndex.Title20Directions));
                    entity.ClientId = StringToInt(GetActualCellValue(row.GetCell((int)CounselingRecordFieldIndex.ClientId))).Value;
                    entity.Tips = GetActualCellValue(row.GetCell((int)CounselingRecordFieldIndex.Tips));
                    entity.OpenDate = StringToDateTime(GetActualCellValue(row.GetCell((int)CounselingRecordFieldIndex.OpenDate)));
                    entity.CloseDate = StringToDateTime(GetActualCellValue(row.GetCell((int)CounselingRecordFieldIndex.CloseDate)));

                    var latestStatus = destinationDbContext.StatusHistories.Where(s => s.CounselingRecordId == entity.Id).OrderByDescending(s => s.Date).FirstOrDefault();
                    entity.StatusId = latestStatus?.Id;

                    entities.Add(entity);
                }

                entities = ConvertDateTimeToUTC(entities);

                destinationDbContext.CounselingRecords.AddRange(entities);
                destinationDbContext.SaveChanges();
            }
            Console.WriteLine("Counseling Records from excel insertion completed...");
            return entities;
        }
        public static void MapCounselingActivityDataFromExcel(ISheet sheet)
        {
            using (var destinationDbContext = new DestinationDbContext())
            {
                List<CounselingActivity> entities = new List<CounselingActivity>();
                var admin = destinationDbContext.AspNetUsers.Single(x => x.UserName == "admin");
                // Iterate through each row
                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);
                    if (row == null) continue;

                    var entity = new CounselingActivity();
                    //entity.Id = StringToInt(GetActualCellValue(row.GetCell((int)CounselingActivityFieldIndex.Id))).Value;
                    entity.Enabled = StringToBool(GetActualCellValue(row.GetCell((int)CounselingActivityFieldIndex.Enabled))).Value;
                    entity.Date = StringToDateTime(GetActualCellValue(row.GetCell((int)CounselingActivityFieldIndex.Date)));
                    entity.Minutes = StringToInt(GetActualCellValue(row.GetCell((int)CounselingActivityFieldIndex.Minutes))) ?? 0;
                    entity.Note = GetActualCellValue(row.GetCell((int)CounselingActivityFieldIndex.Note));
                    entity.EnteredOn = StringToDateTime(GetActualCellValue(row.GetCell((int)CounselingActivityFieldIndex.EnteredOn)));
                    entity.CounselorId = GetCaseWorkerId(GetActualCellValue(row.GetCell((int)CounselingActivityFieldIndex.CounselorId)));
                    entity.CounselingRecordId = StringToInt(GetActualCellValue(row.GetCell((int)CounselingActivityFieldIndex.CounselingRecordId)));
                    entity.ActivityTypeId = StringToInt(GetActualCellValue(row.GetCell((int)CounselingActivityFieldIndex.ActivityTypeId)));
                    entity.SubServiceId = StringToInt(GetActualCellValue(row.GetCell((int)CounselingActivityFieldIndex.SubServiceId)));
                    entity.ProgramInfoId = StringToInt(GetActualCellValue(row.GetCell((int)CounselingActivityFieldIndex.ProgramInfoId)));
                    entity.EnteredById = admin.Id;

                    entities.Add(entity);
                }
                entities = ConvertDateTimeToUTC(entities);
                foreach (var entity in entities)
                {
                    if (entity.CounselingRecordId != null)
                    {
                        entity.SubServiceId = entity.SubServiceId == 1 ? 5 : entity.SubServiceId;
                        entity.SubServiceId = entity.SubServiceId == 2 ? 6 : entity.SubServiceId;
                    }
                }
                // Insert all records
                destinationDbContext.CounselingActivities.AddRange(entities);
                destinationDbContext.SaveChanges();
            }
            Console.WriteLine("Counseling Activities from excel insertion completed..."); 
        }
        public static List<APSRecord> MapAPSRecordDataFromExcel(ISheet sheet)
        {
            List<APSRecord> entities = new List<APSRecord>();

            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                if (row == null) continue;

                var entity = new APSRecord();
                entity.Id = StringToInt(GetActualCellValue(row.GetCell((int)APSRecordFieldIndex.Id))).Value;
                entity.CaseWorkerId = GetCaseWorkerId(GetActualCellValue(row.GetCell((int)APSRecordFieldIndex.CaseWorkerId)));
                entity.ClientId = StringToInt(GetActualCellValue(row.GetCell((int)APSRecordFieldIndex.ClientId))).Value;
                entity.StatusId = StringToInt(GetActualCellValue(row.GetCell((int)APSRecordFieldIndex.StatusId)));
                entity.Tips = GetActualCellValue(row.GetCell((int)APSRecordFieldIndex.Tips));
                entity.OpenDate = StringToDateTime(GetActualCellValue(row.GetCell((int)APSRecordFieldIndex.OpenDate)));
                entity.CloseDate = StringToDateTime(GetActualCellValue(row.GetCell((int)APSRecordFieldIndex.CloseDate)));

                entities.Add(entity);
            }

            entities = ConvertDateTimeToUTC(entities);

            // Insert all records
            using (var destinationDbContext = new DestinationDbContext())
            {
                destinationDbContext.APSRecords.AddRange(entities);
                destinationDbContext.SaveChanges();
            }
            Console.WriteLine("APS Records from excel insertion completed...");
            return entities;
        }
        public static List<StatusHistory> MapStatusHistoryDataFromExcel(ISheet sheet)
        {
            List<StatusHistory> entities = new List<StatusHistory>();
            using (var destinationDbContext = new DestinationDbContext())
            {
                var admin = destinationDbContext.AspNetUsers.Single(x => x.UserName == "admin");
                var counselingActivities = destinationDbContext.CounselingActivities.ToList();
                // Iterate through each row
                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);
                    if (row == null) continue;

                    var entity = new StatusHistory();
                    //entity.Id = StringToInt(GetActualCellValue(row.GetCell((int)StatusHistoryFieldIndex.Id))).Value;
                    entity.StatusId = StringToInt(GetActualCellValue(row.GetCell((int)StatusHistoryFieldIndex.StatusId)));
                    //entity.HomeCareRecordId = StringToInt(GetActualCellValue(row.GetCell((int)StatusHistoryFieldIndex.HomeCareRecordId)));
                    //entity.MealsOnWheelsRecordId = StringToInt(GetActualCellValue(row.GetCell((int)StatusHistoryFieldIndex.MealsOnWheelsRecordId)));
                    //entity.TransportationRecordId = StringToInt(GetActualCellValue(row.GetCell((int)StatusHistoryFieldIndex.TransportationRecordId)));
                    entity.CounselingRecordId = StringToInt(GetActualCellValue(row.GetCell((int)StatusHistoryFieldIndex.CounselingRecordId)));
                    entity.APSRecordId = StringToInt(GetActualCellValue(row.GetCell((int)StatusHistoryFieldIndex.APSRecordId)));
                    entity.SubServiceId = StringToInt(GetActualCellValue(row.GetCell((int)StatusHistoryFieldIndex.SubServiceId)));
                    entity.ProgramInfoId = StringToInt(GetActualCellValue(row.GetCell((int)StatusHistoryFieldIndex.ProgramInfoId)));
                    entity.Date = StringToDateTime(GetActualCellValue(row.GetCell((int)StatusHistoryFieldIndex.Date))).Value;
                    entity.StatusReasonId = StringToInt(GetActualCellValue(row.GetCell((int)StatusHistoryFieldIndex.StatusReasonId)));
                    entity.EnteredById = admin.Id;
                    entity.EnteredOn = StringToDateTime(GetActualCellValue(row.GetCell((int)StatusHistoryFieldIndex.EnteredOn))).Value;

                    entities.Add(entity);
                }
                entities = ConvertDateTimeToUTC(entities);
                foreach(var entity in entities)
                {
                    if (entity.CounselingRecordId != null)
                    {
                        var oldestActivity = counselingActivities.Where(x => x.CounselingRecordId == entity.CounselingRecordId).Min(x => x.Date);
                        entity.SubServiceId = entity.SubServiceId == 1 ? 5 : entity.SubServiceId;
                        entity.SubServiceId = entity.SubServiceId == 2 ? 6 : entity.SubServiceId;
                        entity.StatusId = entity.StatusId == 2 ? 1 : entity.StatusId;
                        entity.Date = oldestActivity.HasValue ? oldestActivity.Value : entity.Date;
                    }
                }

                // Insert all records
                destinationDbContext.StatusHistories.AddRange(entities);
                destinationDbContext.SaveChanges();
            }
            Console.WriteLine("Status Histories from excel insertion completed...");
            return entities;
        }

        public static void MapDataManuallyFromExcel(string fileName)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "SourceDataQuery", fileName);

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // Use NPOI to read the Excel file
                // Create a workbook object from the stream
                XSSFWorkbook workbook = new XSSFWorkbook(stream);
                // Get the first sheet from the workbook
                ISheet sheet1 = workbook.GetSheetAt(0);
                ISheet sheet2 = workbook.GetSheetAt(1);
                ISheet sheet3 = workbook.GetSheetAt(2);
                ISheet sheet4 = workbook.GetSheetAt(3);
                ISheet sheet5 = workbook.GetSheetAt(4);

                MapClientDataFromExcel(sheet1);
                var newCRecords = MapCounselingRecordDataFromExcel(sheet2);
                MapCounselingActivityDataFromExcel(sheet3);
                var newAPSRecords = MapAPSRecordDataFromExcel(sheet4);
                var newStatusHistories = MapStatusHistoryDataFromExcel(sheet5);
                FixStatusForExcelAPSandSA(newAPSRecords, newCRecords, newStatusHistories);

                Console.WriteLine("Manually insertion from excel completed...");
            }
        }

        public static void FixStatusForExcelAPSandSA(List<APSRecord> newAPSRecords, List<CounselingRecord> newCRecords, List<StatusHistory> newStatusHistories)
        {
            using (var destinationDbContext = new DestinationDbContext())
            {
                // post update APS records
                foreach (var aps in newAPSRecords)
                {
                    aps.OpenDate = (aps.OpenDate == null) ? newStatusHistories.Where(x => x.StatusId == 1 && x.APSRecordId == aps.Id)
                        .OrderByDescending(x => x.Date).Select(x => x.Date).FirstOrDefault() : aps.OpenDate;
                    aps.OpenDate = aps.OpenDate.Value != DateTime.MinValue ? aps.OpenDate.Value : null;

                    aps.CloseDate = (aps.CloseDate == null) ? newStatusHistories.Where(x => x.StatusId == 2 && x.APSRecordId == aps.Id)
                        .OrderByDescending(x => x.Date).Select(x => x.Date).FirstOrDefault() : aps.CloseDate;
                    aps.CloseDate = aps.CloseDate.Value != DateTime.MinValue ? aps.CloseDate.Value : null;

                    aps.StatusId = (aps.OpenDate == null && aps.CloseDate != null) ? 2 : aps.StatusId;
                    aps.StatusId = (aps.CloseDate >= aps.OpenDate) ? 2 : aps.StatusId;
                    aps.StatusId = (aps.OpenDate >= aps.CloseDate || (aps.OpenDate != null && aps.CloseDate == null)) ? 1 : aps.StatusId;
                    aps.StatusId = (aps.StatusId == null) ? 3 : aps.StatusId;
                    aps.StatusId = (aps.CloseDate != null && aps.OpenDate == null && aps.StatusId == 3) ? 2 : aps.StatusId;
                    aps.StatusId = (aps.CloseDate == null && aps.OpenDate == null && aps.StatusId != null) ? null : aps.StatusId;
                }
                //var apsRecords = ConvertDateTimeToUTC(apsRecords);
                destinationDbContext.APSRecords.UpdateRange(newAPSRecords);
                destinationDbContext.SaveChanges();
                // post update Counseling records
                foreach (var cr in newCRecords)
                {
                    cr.OpenDate = (cr.OpenDate == null) ? newStatusHistories.Where(x => x.StatusId == 1 && x.CounselingRecordId == cr.Id)
                        .OrderByDescending(x => x.Date).Select(x => x.Date).FirstOrDefault() : cr.OpenDate;
                    cr.OpenDate = cr.OpenDate.Value != DateTime.MinValue ? cr.OpenDate.Value : null;

                    cr.CloseDate = (cr.CloseDate == null) ? newStatusHistories.Where(x => x.StatusId == 2 && x.CounselingRecordId == cr.Id)
                        .OrderByDescending(x => x.Date).Select(x => x.Date).FirstOrDefault() : cr.CloseDate;
                    cr.CloseDate = cr.CloseDate.Value != DateTime.MinValue ? cr.CloseDate.Value : null;

                    cr.StatusId = (cr.OpenDate == null && cr.CloseDate != null) ? 2 : cr.StatusId;
                    cr.StatusId = (cr.CloseDate >= cr.OpenDate) ? 2 : cr.StatusId;
                    cr.StatusId = (cr.OpenDate >= cr.CloseDate || (cr.OpenDate != null && cr.CloseDate == null)) ? 1 : cr.StatusId;
                    cr.StatusId = (cr.StatusId == null) ? 3 : cr.StatusId;
                    cr.StatusId = (cr.CloseDate != null && cr.OpenDate == null && cr.StatusId == 3) ? 2 : cr.StatusId;
                    cr.StatusId = (cr.CloseDate == null && cr.OpenDate == null && cr.StatusId != null) ? null : cr.StatusId;

                    cr.Tips = cr.Tips != null ? (cr.Tips.Length == 0 ? null : cr.Tips) : null;
                }
                //tRecords = ConvertDateTimeToUTC(tRecords);
                destinationDbContext.CounselingRecords.UpdateRange(newCRecords);
                destinationDbContext.SaveChanges();
            }
            Console.WriteLine("Manually fixing statusId from excel completed...");
        }
        #endregion

        #region Cleanup
        public static void MapAssistedWithFromCounselingActivity(string fileName, int counter)
        {
            var path = Path.Combine(AppContext.BaseDirectory, "SourceDataQuery", fileName);
            string query = File.ReadAllText(path);

            var result = new List<CounselingActivity>();
            using (var sdc = new SourceDbContext())
            {
                result = sdc.Set<CounselingActivity>()
                   .FromSqlRaw(query)
                   .AsNoTracking()
                   .ToList();
            }
            //Save counseling assited with table
            result.RemoveAll(x => x.CounselingRecordId == null);
            List<CounselingActivitiesAssistedWith> caAssitesWiths = new List<CounselingActivitiesAssistedWith>();
            int id = 1;
            using (var destinationDbContext = new DestinationDbContext())
            {
                foreach (var ca in result)
                {
                    if (ca.AssistedWithId != null)
                    {
                        caAssitesWiths.Add(new CounselingActivitiesAssistedWith
                        {
                            Id = id++,
                            CounselingActivityId = ca.Id,
                            AssistedWithId = ca.AssistedWithId.Value
                        });
                    }
                }
                destinationDbContext.CounselingActivitiesAssistedWith.AddRange(caAssitesWiths);
                destinationDbContext.SaveChanges();
                Console.WriteLine($"{counter} of 27 completed...");
            }
        }

        public static void PostImportAdditionalOperations()
        {
            using (var destinationDbContext = new DestinationDbContext())
            {
                //Fix name for 2 stafss
                var staffTina = destinationDbContext.Staff.Where(x => x.Id == 139).SingleOrDefault();
                var staffMalinda = destinationDbContext.Staff.Where(x => x.Id == 40).SingleOrDefault();
                staffTina.FirstName = "Chetina";
                staffMalinda.LastName = "OwensWallick";
                destinationDbContext.Staff.Update(staffTina);
                destinationDbContext.Staff.Update(staffMalinda);
                destinationDbContext.SaveChanges();
                //FSSRC AspNetUsers from APSActivities StaffId.txt 
                var users = destinationDbContext.AspNetUsers.ToList();
                var userFN = users.Select(x => x.FirstName).ToList();
                var userLN = users.Select(x => x.LastName).ToList();

                var apsActivityStaffs = destinationDbContext.Staff.Where(x => (new int[] { 24, 40, 12, 94, 119, 98, 99, 69, 49, 122, 118, 131, 132, 139, 170}).Contains(x.Id)).ToList();
                var staffFN = apsActivityStaffs.Select(x => x.FirstName.Split(' ')[0]).ToList();
                var staffLN = apsActivityStaffs.Select(x => x.LastName.Split(' ')[0]).ToList();

                var userIdsToUpdateClaims = destinationDbContext.AspNetUsers.Where(x => (staffFN.Contains(x.FirstName) && staffLN.Contains(x.LastName))).Select( x => x.Id).ToList();

                var apsActivityClaimRecords = destinationDbContext.AspNetUserClaims.Where(x => userIdsToUpdateClaims.Contains(x.UserId)).ToList();
                foreach (var apsActivityClaimRecord in apsActivityClaimRecords)
                {
                    if (!apsActivityClaimRecord.ClaimValue.Contains("AdultProtectiveService"))
                    {
                        apsActivityClaimRecord.ClaimValue += ",AdultProtectiveService";
                    }
                }
                destinationDbContext.AspNetUserClaims.UpdateRange(apsActivityClaimRecords);
                destinationDbContext.SaveChanges();

                //FSSRC Clean up Data.txt 
                var caseNotes = destinationDbContext.CaseNotes.ToList();
                foreach (var caseNote in caseNotes)
                {
                    caseNote.Note = caseNote.Note?.Trim();
                    caseNote.Note = caseNote.Note?.Replace("\"", " ");
                    caseNote.Note = caseNote.Note?.Replace("'", " ");
                    caseNote.Note = caseNote.Note?.Replace("�", " ");
                }
                destinationDbContext.CaseNotes.UpdateRange(caseNotes);
                destinationDbContext.SaveChanges();

                var clients = destinationDbContext.Clients.ToList();
                foreach (var client in clients)
                {
                    client.Tips = client.Tips?.Replace("�", " ");
                    client.MI = client.MI == null ? "" : client.MI;
                    client.PreferredName = client.PreferredName == null ? "" : client.PreferredName;
                    client.Tips = client.Tips == null ? "" : client.Tips;
                    client.HomeAddress = client.HomeAddress == null ? "" : client.HomeAddress;
                    client.City = client.City == null ? "" : client.City;
                    client.StateId = client.StateId == null ? 51 : client.StateId;
                    client.ZipCode = client.ZipCode == null ? "" : client.ZipCode;
                    client.CountyId = client.CountyId == null ? 12 : client.CountyId;
                    client.Email = client.Email == null ? "" : client.Email;
                    client.HomePhone = client.HomePhone == null ? "" : client.HomePhone;
                    client.OtherPhone = client.OtherPhone == null ? "" : client.OtherPhone;
                    client.OtherPhoneDesc = client.OtherPhoneDesc == null ? "" : client.OtherPhoneDesc;
                    client.BillingName = client.BillingName == null ? "" : client.BillingName;
                    client.BillingAddress = client.BillingAddress == null ? "" : client.BillingAddress;
                    client.BillingCity = client.BillingCity == null ? "" : client.BillingCity;
                    client.SocialSecurity = client.SocialSecurity == null ? "" : client.SocialSecurity;
                    client.Medicaid = client.Medicaid == null ? "" : client.Medicaid;
                    client.InsuranceId = client.InsuranceId == null ? "" : client.InsuranceId;
                    client.BillingZip = client.BillingZip == null ? "" : client.BillingZip;
                    client.IntakeStaff = client.IntakeStaff == null ? "" : client.IntakeStaff;
                    client.IfRefferedByOther = client.IfRefferedByOther == null ? "" : client.IfRefferedByOther;
                }
                destinationDbContext.Clients.UpdateRange(clients);
                destinationDbContext.SaveChanges();

                var staffs = destinationDbContext.Staff.ToList();
                foreach (var staff in staffs)
                {
                    staff.Tips = staff.Tips?.Replace("�", " ");
                }
                destinationDbContext.Staff.UpdateRange(staffs);
                destinationDbContext.SaveChanges();

                var homeCareRecords = destinationDbContext.HomeCareRecords.ToList();
                foreach (var homeCareRecord in homeCareRecords)
                {
                    homeCareRecord.Tips = homeCareRecord.Tips?.Replace("�", " ");
                }
                destinationDbContext.HomeCareRecords.UpdateRange(homeCareRecords);
                destinationDbContext.SaveChanges();

                var counselingRecords = destinationDbContext.CounselingRecords.ToList();
                foreach (var counselingRecord in counselingRecords)
                {
                    counselingRecord.Tips = counselingRecord.Tips?.Replace("\"", " ");
                    counselingRecord.Tips = counselingRecord.Tips?.Replace("'", " ");
                }
                destinationDbContext.CounselingRecords.UpdateRange(counselingRecords);
                destinationDbContext.SaveChanges();

                var counselingActivities = destinationDbContext.CounselingActivities.ToList();
                foreach (var counselingActivity in counselingActivities)
                {
                    counselingActivity.Note = counselingActivity.Note?.Replace("�", " ");
                }
                destinationDbContext.CounselingActivities.UpdateRange(counselingActivities);
                destinationDbContext.SaveChanges();

                var APSActivities = destinationDbContext.APSActivities.ToList();
                foreach (var APSActivity in APSActivities)
                {
                    APSActivity.Note = APSActivity.Note?.Replace("�", " ");
                }
                destinationDbContext.APSActivities.UpdateRange(APSActivities);
                destinationDbContext.SaveChanges();

                //FSSRC AspNetUserClaims update per APS and Counseling CaseWorkerId.txt 
                var apsCaseWorkerIds = destinationDbContext.APSRecords.Select(x => x.CaseWorkerId).Distinct().ToList();
                var clientClaimRecords = destinationDbContext.AspNetUserClaims.Where(x => apsCaseWorkerIds.Contains(x.UserId)).ToList();
                foreach (var clientClaimRecord in clientClaimRecords)
                {
                    if (!clientClaimRecord.ClaimValue.Contains("AdultProtectiveService"))
                    {
                        clientClaimRecord.ClaimValue += ",AdultProtectiveService";
                    }
                }
                destinationDbContext.AspNetUserClaims.UpdateRange(clientClaimRecords);
                destinationDbContext.SaveChanges();

                var counselingCaseWorkerIds = destinationDbContext.CounselingRecords.Select(x => x.CaseWorkerId).Distinct().ToList();
                clientClaimRecords = destinationDbContext.AspNetUserClaims.Where(x => counselingCaseWorkerIds.Contains(x.UserId)).ToList();
                foreach (var clientClaimRecord in clientClaimRecords)
                {
                    if (!clientClaimRecord.ClaimValue.Contains("CounselingAndAdvocacy"))
                    {
                        clientClaimRecord.ClaimValue += ",CounselingAndAdvocacy";
                    }
                }
                destinationDbContext.AspNetUserClaims.UpdateRange(clientClaimRecords);
                destinationDbContext.SaveChanges();
            }
            Console.WriteLine("Post import additional operations completed...");
        }
        #endregion

        #region manipulating functions      

        private static List<StatusHistory> StatusHistory_PostDataManipulation(List<StatusHistory> statusHistories)
        {
            using (var destinationDbContext = new DestinationDbContext())
            {
                var admin = destinationDbContext.AspNetUsers.Single(x => x.UserName == "admin");

                var hcRecords = destinationDbContext.HomeCareRecords.ToList();
                var apsRecords = destinationDbContext.APSRecords.ToList();
                var mowRecords = destinationDbContext.MealsOnWheelsRecords.ToList();
                var tRecords = destinationDbContext.TransportationRecords.ToList();
                var cRecords = destinationDbContext.CounselingRecords.ToList();
                foreach (var currentRecord in statusHistories)
                {
                    currentRecord.HomeCareRecordId = hcRecords.Where(x => x.ClientId == currentRecord.HomeCareRecordId).Select(x => (int?)x.Id).FirstOrDefault();
                    currentRecord.APSRecordId = apsRecords.Where(x => x.ClientId == currentRecord.APSRecordId).Select(x => (int?)x.Id).FirstOrDefault();
                    currentRecord.MealsOnWheelsRecordId = mowRecords.Where(x => x.ClientId == currentRecord.MealsOnWheelsRecordId).Select(x => (int?)x.Id).FirstOrDefault();
                    currentRecord.TransportationRecordId = tRecords.Where(x => x.ClientId == currentRecord.TransportationRecordId).Select(x => (int?)x.Id).FirstOrDefault();
                    currentRecord.CounselingRecordId = cRecords.Where(x => x.ClientId == currentRecord.CounselingRecordId).Select(x => (int?)x.Id).FirstOrDefault();
                    currentRecord.EnteredById = admin.Id;
                }

                // post update APS records
                foreach (var aps in apsRecords)
                {
                    aps.OpenDate = (aps.OpenDate == null) ? statusHistories.Where(x => x.StatusId == 1 && x.APSRecordId == aps.Id)
                        .OrderByDescending(x => x.Date).Select(x => x.Date).FirstOrDefault() : aps.OpenDate;
                    aps.OpenDate = aps.OpenDate.Value != DateTime.MinValue ? aps.OpenDate.Value.AddHours(-TimeZoneInfo.FindSystemTimeZoneById("America/Chicago").GetUtcOffset(aps.OpenDate.Value).TotalHours) : null;

                    aps.CloseDate = (aps.CloseDate == null) ? statusHistories.Where(x => x.StatusId == 2 && x.APSRecordId == aps.Id)
                        .OrderByDescending(x => x.Date).Select(x => x.Date).FirstOrDefault() : aps.CloseDate;
                    aps.CloseDate = aps.CloseDate.Value != DateTime.MinValue ? aps.CloseDate.Value.AddHours(-TimeZoneInfo.FindSystemTimeZoneById("America/Chicago").GetUtcOffset(aps.CloseDate.Value).TotalHours) : null;

                    aps.StatusId = (aps.OpenDate == null && aps.CloseDate != null) ? 2 : aps.StatusId;
                    aps.StatusId = (aps.CloseDate >= aps.OpenDate) ? 2 : aps.StatusId;
                    aps.StatusId = (aps.OpenDate >= aps.CloseDate || (aps.OpenDate != null && aps.CloseDate == null)) ? 1 : aps.StatusId;
                    aps.StatusId = (aps.StatusId == null) ? 3 : aps.StatusId;
                    aps.StatusId = (aps.CloseDate != null && aps.OpenDate == null && aps.StatusId == 3) ? 2 : aps.StatusId;
                    aps.StatusId = (aps.CloseDate == null && aps.OpenDate == null && aps.StatusId != null) ? null : aps.StatusId;
                }
                //var apsRecords = ConvertDateTimeToUTC(apsRecords);
                destinationDbContext.APSRecords.UpdateRange(apsRecords);
                destinationDbContext.SaveChanges();

                // post update Transportation records
                foreach (var tr in tRecords)
                {
                    tr.OpenDate = (tr.OpenDate == null) ? statusHistories.Where(x => x.StatusId == 1 && x.TransportationRecordId == tr.Id)
                        .OrderByDescending(x => x.Date).Select(x => x.Date).FirstOrDefault() : tr.OpenDate;
                    tr.OpenDate = tr.OpenDate.Value != DateTime.MinValue ? tr.OpenDate.Value.AddHours(-TimeZoneInfo.FindSystemTimeZoneById("America/Chicago").GetUtcOffset(tr.OpenDate.Value).TotalHours) : null;

                    tr.CloseDate = (tr.CloseDate == null) ? statusHistories.Where(x => x.StatusId == 2 && x.TransportationRecordId == tr.Id)
                        .OrderByDescending(x => x.Date).Select(x => x.Date).FirstOrDefault() : tr.CloseDate;
                    tr.CloseDate = tr.CloseDate.Value != DateTime.MinValue ? tr.CloseDate.Value.AddHours(-TimeZoneInfo.FindSystemTimeZoneById("America/Chicago").GetUtcOffset(tr.CloseDate.Value).TotalHours) : null;

                    tr.StatusId = (tr.OpenDate == null && tr.CloseDate != null) ? 2 : tr.StatusId;
                    tr.StatusId = (tr.CloseDate >= tr.OpenDate) ? 2 : tr.StatusId;
                    tr.StatusId = (tr.OpenDate >= tr.CloseDate || (tr.OpenDate != null && tr.CloseDate == null)) ? 1 : tr.StatusId;
                    tr.StatusId = (tr.StatusId == null) ? 3 : tr.StatusId;
                    tr.StatusId = (tr.CloseDate != null && tr.OpenDate == null && tr.StatusId == 3) ? 2 : tr.StatusId;
                    tr.StatusId = (tr.CloseDate == null && tr.OpenDate == null && tr.StatusId != null) ? null : tr.StatusId;
                }
                //tRecords = ConvertDateTimeToUTC(tRecords);
                destinationDbContext.TransportationRecords.UpdateRange(tRecords);
                destinationDbContext.SaveChanges();

                // post update Counseling records
                foreach (var cr in cRecords)
                {
                    cr.OpenDate = (cr.OpenDate == null) ? statusHistories.Where(x => x.StatusId == 1 && x.CounselingRecordId == cr.Id)
                        .OrderByDescending(x => x.Date).Select(x => x.Date).FirstOrDefault() : cr.OpenDate;
                    cr.OpenDate = cr.OpenDate.Value != DateTime.MinValue ? cr.OpenDate.Value.AddHours(-TimeZoneInfo.FindSystemTimeZoneById("America/Chicago").GetUtcOffset(cr.OpenDate.Value).TotalHours) : null;

                    cr.CloseDate = (cr.CloseDate == null) ? statusHistories.Where(x => x.StatusId == 2 && x.CounselingRecordId == cr.Id)
                        .OrderByDescending(x => x.Date).Select(x => x.Date).FirstOrDefault() : cr.CloseDate;
                    cr.CloseDate = cr.CloseDate.Value != DateTime.MinValue ? cr.CloseDate.Value.AddHours(-TimeZoneInfo.FindSystemTimeZoneById("America/Chicago").GetUtcOffset(cr.CloseDate.Value).TotalHours) : null;

                    cr.StatusId = (cr.OpenDate == null && cr.CloseDate != null) ? 2 : cr.StatusId;
                    cr.StatusId = (cr.CloseDate >= cr.OpenDate) ? 2 : cr.StatusId;
                    cr.StatusId = (cr.OpenDate >= cr.CloseDate || (cr.OpenDate != null && cr.CloseDate == null)) ? 1 : cr.StatusId;
                    cr.StatusId = (cr.StatusId == null) ? 3 : cr.StatusId;
                    cr.StatusId = (cr.CloseDate != null && cr.OpenDate == null && cr.StatusId == 3) ? 2 : cr.StatusId;
                    cr.StatusId = (cr.CloseDate == null && cr.OpenDate == null && cr.StatusId != null) ? null : cr.StatusId;

                    cr.Tips = cr.Tips != null ? (cr.Tips.Length == 0 ? null : cr.Tips) : null;
                }
                //tRecords = ConvertDateTimeToUTC(tRecords);
                destinationDbContext.CounselingRecords.UpdateRange(cRecords);
                destinationDbContext.SaveChanges();

                // post update Transportation records
                foreach (var mr in mowRecords)
                {
                    mr.OpenDate = (mr.OpenDate == null) ? statusHistories.Where(x => x.StatusId == 1 && x.MealsOnWheelsRecordId == mr.Id)
                        .OrderByDescending(x => x.Date).Select(x => x.Date).FirstOrDefault() : mr.OpenDate;
                    mr.OpenDate = mr.OpenDate.Value != DateTime.MinValue ? mr.OpenDate.Value.AddHours(-TimeZoneInfo.FindSystemTimeZoneById("America/Chicago").GetUtcOffset(mr.OpenDate.Value).TotalHours) : null;

                    mr.CloseDate = (mr.CloseDate == null) ? statusHistories.Where(x => x.StatusId == 2 && x.MealsOnWheelsRecordId == mr.Id)
                        .OrderByDescending(x => x.Date).Select(x => x.Date).FirstOrDefault() : mr.CloseDate;
                    mr.CloseDate = mr.CloseDate.Value != DateTime.MinValue ? mr.CloseDate.Value.AddHours(-TimeZoneInfo.FindSystemTimeZoneById("America/Chicago").GetUtcOffset(mr.CloseDate.Value).TotalHours) : null;

                    mr.StatusId = (mr.OpenDate == null && mr.CloseDate != null) ? 2 : mr.StatusId;
                    mr.StatusId = (mr.CloseDate >= mr.OpenDate) ? 2 : mr.StatusId;
                    mr.StatusId = (mr.OpenDate >= mr.CloseDate || (mr.OpenDate != null && mr.CloseDate == null)) ? 1 : mr.StatusId;
                    mr.StatusId = (mr.StatusId == null) ? 3 : mr.StatusId;
                    mr.StatusId = (mr.CloseDate != null && mr.OpenDate == null && mr.StatusId == 3) ? 2 : mr.StatusId;
                    mr.StatusId = (mr.CloseDate == null && mr.OpenDate == null && mr.StatusId != null) ? null : mr.StatusId;
                }
                //tRecords = ConvertDateTimeToUTC(tRecords);
                destinationDbContext.MealsOnWheelsRecords.UpdateRange(mowRecords);
                destinationDbContext.SaveChanges();

                // post update Homecare records
                foreach (var hc in hcRecords)
                {
                    hc.StatusId = (hc.StatusId == null) ? statusHistories.Where(x => x.HomeCareRecordId == hc.Id)
                        .OrderByDescending(x => x.Date).Select(x => x.StatusId).FirstOrDefault() : hc.StatusId;
                    hc.StatusId = (hc.StatusId == null) ? 3 : hc.StatusId;
                }
                //hcRecords = ConvertDateTimeToUTC(hcRecords);
                destinationDbContext.HomeCareRecords.UpdateRange(hcRecords);
                destinationDbContext.SaveChanges();
            }

            statusHistories.RemoveAll(x => statusHistories.Where(y => y.StatusId == null).ToList().Contains(x)); ;
            return statusHistories;
        }

        private static List<CaseNote> CaseNote_PostDataManipulation(List<CaseNote> caseNotes)
        {
            using (var destinationDbContext = new DestinationDbContext())
            {
                var admin = destinationDbContext.AspNetUsers.Single(x => x.UserName == "admin");
                var staffs = destinationDbContext.Staff.ToList();
                var users = destinationDbContext.AspNetUsers.ToList();
                var hcRecords = destinationDbContext.HomeCareRecords.ToList();
                var mowRecords = destinationDbContext.MealsOnWheelsRecords.ToList();
                var tRecords = destinationDbContext.TransportationRecords.ToList();

                foreach (var currentRecord in caseNotes)
                {
                    var staff = staffs.SingleOrDefault(x => x.Id == currentRecord.StaffId);
                    currentRecord.EnteredById = admin.Id;
                    currentRecord.UserId = users.Where(x => x.FirstName == staff?.FirstName && x.LastName == staff?.LastName)
                        .Select(x => (Guid?)x.Id).FirstOrDefault();

                    // As HomeCareRecordId/MealsOnWheelsRecordId/TransportationRecordId were populated with related client id
                    // on the source db we need to update id to actual id from destination db
                    currentRecord.HomeCareRecordId = hcRecords.Where(x => x.ClientId == currentRecord.HomeCareRecordId).Select(x => (int?)x.Id).FirstOrDefault();
                    currentRecord.MealsOnWheelsRecordId = mowRecords.Where(x => x.ClientId == currentRecord.MealsOnWheelsRecordId).Select(x => (int?)x.Id).FirstOrDefault();
                    currentRecord.TransportationRecordId = tRecords.Where(x => x.ClientId == currentRecord.TransportationRecordId).Select(x => (int?)x.Id).FirstOrDefault();
                }
            }

            return caseNotes;
        }

        private static List<APSActivity> APSActivity_PostDataManipulation(List<APSActivity> apsActivities)
        {
            using (var destinationDbContext = new DestinationDbContext())
            {
                var users = destinationDbContext.AspNetUsers.ToList();
                var apsRecords = destinationDbContext.APSRecords.ToList();
                var sis = destinationDbContext.SIS.ToList();
                foreach (var currentRecord in apsActivities)
                {
                    currentRecord.Minutes = currentRecord.Minutes.HasValue && currentRecord.Minutes.Value > 0 ? currentRecord.Minutes : 0;
                    currentRecord.UserId = users.Where(x => x.UserName == currentRecord.UserName).Select(x => x.Id).FirstOrDefault();
                    currentRecord.APSRecordId = apsRecords.Where(x => x.ClientId == currentRecord.ClientId).Select(x => x.Id).FirstOrDefault();
                    currentRecord.UserId = currentRecord.UserId == Guid.Empty ? null : currentRecord.UserId;
                    currentRecord.SISId = sis.Any(x => x.Id == currentRecord.SISId) ? currentRecord.SISId : null;
                }
            }

            return apsActivities;
        }

        private static List<APSRecord> APSRecord_PostDataManipulation(List<APSRecord> apsRecords)
        {
            using (var destinationDbContext = new DestinationDbContext())
            {
                var users = destinationDbContext.AspNetUsers.ToList();
                foreach (var currentRecord in apsRecords)
                {
                    currentRecord.CaseWorkerId = users.Where(x => x.UserName == currentRecord.CaseWorkerUserName).Select(x => x.Id).FirstOrDefault();
                    currentRecord.CaseWorkerId = currentRecord.CaseWorkerId == Guid.Empty ? null : currentRecord.CaseWorkerId;
                    currentRecord.Tips = currentRecord.Tips.IsNullOrEmpty() ? null : currentRecord.Tips;
                }
            }

            apsRecords.RemoveAll(x => !apsRecords.GroupBy(x => x.ClientId).Select(g => g.First()).ToList().Contains(x));
            return apsRecords;
        }

        private static List<CounselingActivity> CounselingActivity_PostDataManipulation(List<CounselingActivity> counselingActivities)
        {
            using (var destinationDbContext = new DestinationDbContext())
            {
                var admin = destinationDbContext.AspNetUsers.Single(x => x.UserName == "admin");

                var users = destinationDbContext.AspNetUsers.ToList();
                var cRecords = destinationDbContext.CounselingRecords.ToList();
                foreach (var ca in counselingActivities)
                {
                    ca.CounselorId = users.Where(x => x.UserName == ca.CounselorUserName).Select(x => x.Id).FirstOrDefault();
                    ca.CounselorId = ca.CounselorId == Guid.Empty ? null : ca.CounselorId;
                    ca.CounselingRecordId = cRecords.Where(x => x.ClientId == ca.CounselingRecordId).Select(x => (int?)x.Id).FirstOrDefault();
                    ca.EnteredById = admin.Id;
                    ca.Minutes = ca.Minutes ?? 0;
                }
            }

            counselingActivities.RemoveAll(x => x.CounselingRecordId == null);

            return counselingActivities;
        }

        private static List<CounselingRecord> CounselingRecord_PostDataManipulation(List<CounselingRecord> counselingRecords)
        {
            using (var destinationDbContext = new DestinationDbContext())
            {
                var users = destinationDbContext.AspNetUsers.ToList();
                foreach (var cr in counselingRecords)
                {
                    cr.CaseWorkerId = users.Where(x => x.UserName == cr.CaseWorkerUserName).Select(x => x.Id).FirstOrDefault();
                    cr.CaseWorkerId = cr.CaseWorkerId == Guid.Empty ? null : cr.CaseWorkerId;
                    cr.Title20Directions = cr.Title20Directions.IsNullOrEmpty() ? null : cr.Title20Directions;
                    cr.Tips = cr.Tips.IsNullOrEmpty() ? null : cr.Tips;
                }
            }

            counselingRecords.RemoveAll(x => !counselingRecords.GroupBy(x => x.ClientId).Select(g => g.First()).ToList().Contains(x));
            return counselingRecords;
        }

        private static List<CaseManager> CaseManager_PostDataManipulation(List<CaseManager> caseManagers)
        {
            int casemanagerId = 240;
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Brandy",
                LastName = "Sprout",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Bruce",
                LastName = "Butler",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "C/M Case",
                LastName = "Manager",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Heather",
                LastName = "Anderson",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "HomeCare",
                LastName = "Staff",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Kiley",
                LastName = "(Temp)",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Lori",
                LastName = "Blacker",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Lynn",
                LastName = "Jacobs",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Matt",
                LastName = "Hollenback",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Robin",
                LastName = "Way",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Sara",
                LastName = "Short",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Sonovia",
                LastName = "Britt",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Tami",
                LastName = "Siddens",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Phone",
                LastName = "Information Referral Staff",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Rhonda",
                LastName = "Jarrett",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Dina",
                LastName = "Gray",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Kristin",
                LastName = "Parchim",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Stephanie",
                LastName = "Bean",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Crystal",
                LastName = "Dillow",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Gina",
                LastName = "Roppa",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Jacqueline",
                LastName = "Walsh",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Nancy",
                LastName = "Larson",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Elaine",
                LastName = "Warner",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Ruth",
                LastName = "Arbogast",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Kerry",
                LastName = "Bell Cumberland",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Kerry",
                LastName = "Bell",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Glenn",
                LastName = "Gentry",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Hollee",
                LastName = "Pattison",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Lori",
                LastName = "Noe",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Jackie",
                LastName = "Davis",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Elaine",
                LastName = "Schlorff",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Sharon",
                LastName = "Draper",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Kara",
                LastName = "Mehl",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Michelle",
                LastName = "Drake",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Lindsey",
                LastName = "Wieneke",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Laurie",
                LastName = "Howard",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Lindsey",
                LastName = "Wienke",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Jacqueline",
                LastName = "Davis",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Kathleen",
                LastName = "May",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Jean",
                LastName = "Parker",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Kelly",
                LastName = "Hammond",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Katie",
                LastName = "Hettinger",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Bill",
                LastName = "Geis",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Gretchen",
                LastName = "Harney",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Patti",
                LastName = "Reichard",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Laurie",
                LastName = "Edwards",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Janet",
                LastName = "Marshall",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Lindsey",
                LastName = "W.",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Gina",
                LastName = "",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Anna",
                LastName = "",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Nancy",
                LastName = "",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Glenn",
                LastName = "",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Lori",
                LastName = "",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Heather",
                LastName = "",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Bill",
                LastName = "",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Lindsey",
                LastName = "",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            caseManagers.Add(new CaseManager
            {
                Id = casemanagerId,
                FirstName = "Kelly",
                LastName = "",
                PhoneNumber = null,
                Email = null,
                Agency = null,
                Enabled = false,
                DisplayOrder = casemanagerId++
            });
            using (var destinationDbContext = new DestinationDbContext())
            {
                var users = destinationDbContext.AspNetUsers.ToList();
                foreach (var caseManager in caseManagers)
                {
                    char[] delimiter = { ' ', '(', '-', ',' };
                    var correspondingUser = users.Where(x => x.FirstName.Split(delimiter)[0] == caseManager.FirstName.Split(delimiter)[0]
                        && x.LastName.Split(delimiter)[0] == caseManager.LastName.Split(delimiter)[0]).FirstOrDefault();
                    if(correspondingUser != null)
                    {
                        caseManager.Email = correspondingUser.Email;
                        caseManager.FirstName = correspondingUser.FirstName;
                        caseManager.LastName = correspondingUser.LastName;
                    }
                }
            }
            var duplicateCaseManagers = caseManagers.GroupBy(x => new { x.FirstName, x.LastName }).Where(x => x.Count() > 1).Select(x => x.ToList()).ToList();
            foreach(var duplicateCM in duplicateCaseManagers)
            {
                var numberOfItems = duplicateCM.Count();
                for(int i = 1;i < numberOfItems; i++)
                {
                    var removableCM = duplicateCM[i];
                    caseManagers.Remove(removableCM);
                }
            }
            
            return caseManagers;
        }

        private static List<Client> Client_PostDataManipulation(List<Client> clients)
        {
            foreach (var client in clients)
            {
                // encrypting ssn
                if (!client.SocialSecurity.IsNullOrEmpty())
                {
                    client.SocialSecurity = CryptographyService.EncryptString(client.SocialSecurity);
                }

            }

            return clients;
        }

        private static List<InServiceInformation> InserviceInfo_PostDataManipulation(List<InServiceInformation> inServiceInfos)
        {
            using (var destinationDbContext = new DestinationDbContext())
            {
                var admin = destinationDbContext.AspNetUsers.Single(x => x.UserName == "admin");

                foreach (var inServiceInfo in inServiceInfos)
                {
                    inServiceInfo.EnteredById = admin.Id.ToString();
                    var staffRecord = destinationDbContext.Staff.Single(x => x.Id == inServiceInfo.StaffId);
                    inServiceInfo.PayRate = inServiceInfo.PayRate != 0 ? inServiceInfo.PayRate : (staffRecord.PayrollRate != null ? (double)staffRecord.PayrollRate : 0);
                }
            }
            return inServiceInfos;
        }

        private static List<HomeCareRecord> HomeCareRecord_PostDataManipulation(List<HomeCareRecord> homeCareRecords)
        {
            using (var destinationDbContext = new DestinationDbContext())
            {
                foreach (var homeCareRecord in homeCareRecords)
                {
                    if (homeCareRecord.CaseManagerName != null)
                    {
                        var caseManagerLN = string.Empty;
                        var caseManagerFN = string.Empty;
                        if (homeCareRecord.CaseManagerName.Split(' ').Count() > 1)
                        {
                            if (homeCareRecord.CaseManagerName.Split('(').Count() > 1)
                            {
                                homeCareRecord.CaseManagerName = homeCareRecord.CaseManagerName.Split('(').Count() == 2 ? (homeCareRecord.CaseManagerName.Split('(')[0]).TrimEnd() : (homeCareRecord.CaseManagerName.Split('(')[0] + "(" + homeCareRecord.CaseManagerName.Split('(')[1]).TrimEnd();
                            }
                            caseManagerFN = homeCareRecord.CaseManagerName.Split(' ')[0];
                            caseManagerLN = homeCareRecord.CaseManagerName.Split(' ')[1];
                            if (homeCareRecord.CaseManagerName.Split(' ').Count() > 2)
                            {
                                caseManagerLN += " " + homeCareRecord.CaseManagerName.Split(' ')[2];
                                if (homeCareRecord.CaseManagerName.Split(' ').Count() > 3)
                                {
                                    caseManagerLN += " " + homeCareRecord.CaseManagerName.Split(' ')[3];
                                }
                            }
                        }
                        else
                        {
                            caseManagerFN = homeCareRecord.CaseManagerName;
                        }
                        var caseManager = destinationDbContext.CaseManagers.Where(x => x.FirstName + " " + x.LastName == caseManagerFN + " " + caseManagerLN).SingleOrDefault();
                        if (caseManager != null)
                        {
                            homeCareRecord.CaseManagerId = caseManager.Id;
                        }
                        else
                        {
                            homeCareRecord.CaseManagerId = null;
                        }
                    }
                }
            }
            return homeCareRecords;
        }

        private static List<MealsOnWheelsRecord> MealsOnWheelsRecord_PostDataManipulation(List<MealsOnWheelsRecord> mealsOnWheelsRecords)
        {
            using (var destinationDbContext = new DestinationDbContext())
            {
                foreach (var mealsOnWheelsRecord in mealsOnWheelsRecords)
                {
                    if (mealsOnWheelsRecord.CaseManagerName != null)
                    {
                        mealsOnWheelsRecord.CaseManagerName = mealsOnWheelsRecord.CaseManagerName.Split('(').Count() == 2 ? (mealsOnWheelsRecord.CaseManagerName.Split('(')[0]).TrimEnd() : (mealsOnWheelsRecord.CaseManagerName.Split('(')[0] + "(" + mealsOnWheelsRecord.CaseManagerName.Split('(')[1]).TrimEnd();
                        var caseManagerFN = mealsOnWheelsRecord.CaseManagerName.Split(' ')[0];
                        var caseManagerLN = mealsOnWheelsRecord.CaseManagerName.Split(' ')[1];
                        if (mealsOnWheelsRecord.CaseManagerName.Split(' ').Count() > 2)
                        {
                            caseManagerLN += " " + mealsOnWheelsRecord.CaseManagerName.Split(' ')[2];
                        }
                        var caseManager = destinationDbContext.CaseManagers.Where(x => x.FirstName + " " + x.LastName == caseManagerFN + " " + caseManagerLN).SingleOrDefault();
                        if (caseManager != null)
                        {
                            mealsOnWheelsRecord.CaseManagerId = caseManager.Id;
                        }
                        else
                        {
                            mealsOnWheelsRecord.CaseManagerId = null;
                        }
                    }
                }
            }
            return mealsOnWheelsRecords;
        }

        private static List<TransportationRecord> TransportationRecord_PostDataManipulation(List<TransportationRecord> transportationRecords)
        {
            using (var destinationDbContext = new DestinationDbContext())
            {
                foreach (var transportationRecord in transportationRecords)
                {
                    if (transportationRecord.CaseManagerName != null)
                    {
                        transportationRecord.CaseManagerName = transportationRecord.CaseManagerName.Split('(').Count() == 2 ? (transportationRecord.CaseManagerName.Split('(')[0]).TrimEnd() : (transportationRecord.CaseManagerName.Split('(')[0] + "(" + transportationRecord.CaseManagerName.Split('(')[1]).TrimEnd();
                        var caseManagerFN = transportationRecord.CaseManagerName.Split(' ')[0];
                        var caseManagerLN = transportationRecord.CaseManagerName.Split(' ')[1];
                        if (transportationRecord.CaseManagerName.Split(' ').Count() > 2)
                        {
                            caseManagerLN += " " + transportationRecord.CaseManagerName.Split(' ')[2];
                            if (transportationRecord.CaseManagerName.Split(' ').Count() > 3)
                            {
                                caseManagerLN += " " + transportationRecord.CaseManagerName.Split(' ')[3];
                            }
                        }
                        var caseManager = destinationDbContext.CaseManagers.Where(x => x.FirstName + " " + x.LastName == caseManagerFN + " " + caseManagerLN).SingleOrDefault();
                        if (caseManager != null)
                        {
                            transportationRecord.CaseManagerId = caseManager.Id;
                        }
                        else
                        {
                            transportationRecord.CaseManagerId = null;
                        }
                    }
                }
            }
            return transportationRecords;
        }

        private static List<ScheduleEvent> ScheduleEvent_PostDataManipulation(List<ScheduleEvent> scheduleEvents)
        {
            using (var destinationDbContext = new DestinationDbContext())
            {
                DateTime today = DateTime.Today;
                int daysUntilSunday = ((int)DayOfWeek.Sunday - (int)today.DayOfWeek + 7) % 7;
                DateTime lastSunday = today.AddDays(-(7 - daysUntilSunday));
                foreach (var scheduleEvent in scheduleEvents)
                {
                    var client = destinationDbContext.Clients.Single(x => x.Id == scheduleEvent.ClientId);
                    scheduleEvent.Subject = client.LastName + ", " + client.FirstName;

                    bool hasValidStaff = destinationDbContext.Staff.Any(x => x.Id == scheduleEvent.StaffId);
                    scheduleEvent.StaffId = hasValidStaff ? scheduleEvent.StaffId : null;

                    if (scheduleEvent.RecurrenceRule != null)
                    {
                        string dayOfWeek = scheduleEvent.RecurrenceRule.Split(';')[1].Split('=')[1];
                        switch (dayOfWeek)
                        {
                            case "SU":
                                scheduleEvent.StartTime = lastSunday.Date.Add((TimeSpan)scheduleEvent.StartTime?.TimeOfDay);
                                scheduleEvent.EndTime = lastSunday.Date.Add((TimeSpan)scheduleEvent.EndTime?.TimeOfDay);
                                break;
                            case "MO":
                                scheduleEvent.StartTime = lastSunday.Date.AddDays(1).Add((TimeSpan)scheduleEvent.StartTime?.TimeOfDay);
                                scheduleEvent.EndTime = lastSunday.Date.AddDays(1).Add((TimeSpan)scheduleEvent.EndTime?.TimeOfDay);
                                break;
                            case "TU":
                                scheduleEvent.StartTime = lastSunday.Date.AddDays(2).Add((TimeSpan)scheduleEvent.StartTime?.TimeOfDay);
                                scheduleEvent.EndTime = lastSunday.Date.AddDays(2).Add((TimeSpan)scheduleEvent.EndTime?.TimeOfDay);
                                break;
                            case "WE":
                                scheduleEvent.StartTime = lastSunday.Date.AddDays(3).Add((TimeSpan)scheduleEvent.StartTime?.TimeOfDay);
                                scheduleEvent.EndTime = lastSunday.Date.AddDays(3).Add((TimeSpan)scheduleEvent.EndTime?.TimeOfDay);
                                break;
                            case "TH":
                                scheduleEvent.StartTime = lastSunday.Date.AddDays(4).Add((TimeSpan)scheduleEvent.StartTime?.TimeOfDay);
                                scheduleEvent.EndTime = lastSunday.Date.AddDays(4).Add((TimeSpan)scheduleEvent.EndTime?.TimeOfDay);
                                break;
                            case "FR":
                                scheduleEvent.StartTime = lastSunday.Date.AddDays(5).Add((TimeSpan)scheduleEvent.StartTime?.TimeOfDay);
                                scheduleEvent.EndTime = lastSunday.Date.AddDays(5).Add((TimeSpan)scheduleEvent.EndTime?.TimeOfDay);
                                break;
                            case "SA":
                                scheduleEvent.StartTime = lastSunday.Date.AddDays(6).Add((TimeSpan)scheduleEvent.StartTime?.TimeOfDay);
                                scheduleEvent.EndTime = lastSunday.Date.AddDays(6).Add((TimeSpan)scheduleEvent.EndTime?.TimeOfDay);
                                break;
                            default: break;
                        }
                        scheduleEvent.RecurrenceRule += "INTERVAL=1;";
                    }
                }
            }
            return scheduleEvents;
        }

        private static List<ActualEvent> ActualEvent_PostDataManipulation(List<ActualEvent> actualEvents)
        {
            using (var destinationDbContext = new DestinationDbContext())
            {
                foreach (var actualEvent in actualEvents)
                {
                    bool hasValidStaff = destinationDbContext.Staff.Any(x => x.Id == actualEvent.StaffId);
                    actualEvent.StaffId = hasValidStaff ? actualEvent.StaffId : null;
                }
            }
            return actualEvents;
        }

        private static List<Expense> Expense_PostDataManipulation(List<Expense> expenses)
        {
            using (var destinationDbContext = new DestinationDbContext())
            {
                foreach (var expense in expenses)
                {
                    var homeCareRecord = destinationDbContext.HomeCareRecords.Where(x => x.ClientId == expense.HomeCareRecordId).SingleOrDefault();
                    expense.HomeCareRecordId = homeCareRecord.Id;
                }
            }
            return expenses;
        }

        private static List<CareTypeValue> CareTypeValue_PostDataManipulation(List<CareTypeValue> careTypeValues)
        {
            using (var destinationDbContext = new DestinationDbContext())
            {
                foreach (var careTypeValue in careTypeValues)
                {
                    var homeCareRecord = destinationDbContext.HomeCareRecords.Where(x => x.ClientId == careTypeValue.HomeCareRecordId).SingleOrDefault();
                    careTypeValue.HomeCareRecordId = homeCareRecord.Id;
                }
            }
            return careTypeValues;
        }
        private static List<RouteInfo> RouteInformation_PostDataManipulation(List<RouteInfo> routeInfos)
        {
            using (var destinationDbContext = new DestinationDbContext())
            {
                foreach (var routeInfo in routeInfos)
                {
                    var MOWRecord = destinationDbContext.MealsOnWheelsRecords.Where(x => x.ClientId == routeInfo.MealsOnWheelsRecordId).First();
                    routeInfo.MealsOnWheelsRecordId = MOWRecord.Id;
                }
            }
            return routeInfos;
        }
        private static List<TransportationActivity> TransportationActivity_PostDataManipulation(List<TransportationActivity> transportationActivities)
        {
            using (var destinationDbContext = new DestinationDbContext())
            {
                foreach (var transportationActivity in transportationActivities)
                {
                    bool hasValidStaff = destinationDbContext.Staff.Any(x => x.Id == transportationActivity.StaffId);
                    transportationActivity.StaffId = hasValidStaff ? transportationActivity.StaffId : null;

                    var transportationRecord = destinationDbContext.TransportationRecords.Where(x => x.ClientId == transportationActivity.TransportationRecordId).First();

                    transportationActivity.TransportationRecordId = transportationRecord.Id;
                }
            }
            return transportationActivities;
        }
        #endregion

        #region String Converters
        private static int? StringToInt(string? str)
        {
            str = FormatString(str);
            if (str == null) return null;
            bool canConvert = int.TryParse(str, out int intValue);
            if(!canConvert) return null;
            return intValue;
        }
        private static Guid? StringToGuid(string? str)
        {
            str = FormatString(str);
            if (str == null) return null;
            return new Guid(str);
        }
        private static double? StringToDouble(string? str)
        {
            str = FormatString(str);
            if (str == null) return null;
            return Convert.ToDouble(str);
        }
        private static bool? StringToBool(string? str)
        {
            str = FormatString(str);
            if (str == null) return null;
            return str == "0" ? false : true;
        }
        private static DateTime? StringToDateTime(string? str)
        {
            str = FormatString(str);
            if (str == null) return null;
            return Convert.ToDateTime(str);
        }
        private static string? FormatString(string? str) 
        {
            if (str == null) return null;
            str = str.Trim();
            if (str == "") return null;
            return str;
        }

        private static string? GetActualCellValue(NPOI.SS.UserModel.ICell cell)
        {
            string? cellValue = null;
            if (cell == null) return null;
            switch (cell.CellType)
            {
                case CellType.Boolean:
                    cellValue = cell.BooleanCellValue.ToString();
                    break;
                case CellType.Numeric:
                    if (DateUtil.IsCellDateFormatted(cell))
                    {
                        cellValue = cell.DateCellValue.ToString();
                    }
                    else
                    {
                        cellValue = cell.NumericCellValue.ToString();
                    }
                    break;
                case CellType.String:
                    cellValue = cell.StringCellValue.Trim();
                    break;
                case CellType.Formula:
                    switch (cell.CachedFormulaResultType)
                    {
                        case CellType.Boolean:
                            cellValue = cell.BooleanCellValue.ToString();
                            break;
                        case CellType.Numeric:
                            cellValue = cell.NumericCellValue.ToString();
                            break;
                        case CellType.String:
                            cellValue = cell.StringCellValue.Trim();
                            break;
                    }
                    break;
            }
            return cellValue;
        }

        private static Guid? GetCaseWorkerId(string? caseWorkerName)
        {
            if(caseWorkerName == null) return null;
            Dictionary<string, string> counselorNameToEmail = new Dictionary<string, string>()
                {
                    {"BeckiAnderson", "BAnderson@familyservicecc.org"},
                    {"Katherine(Molina)Akers", "KAkers@familyservicecc.org"},
                    {"VickiAnderson", "VAnderson@familyservicecc.org"}
                };
            using (var destinationDbContext = new DestinationDbContext())
            {
                var caseWorkerId = destinationDbContext.AspNetUsers.Where(x => x.Email == counselorNameToEmail[caseWorkerName]).Select(x => x.Id).SingleOrDefault();
                return caseWorkerId;
            }
        }
        #endregion
    }

    #region Excel FieldName to Index mapping Enums
    public enum ClientFieldIndex
    {
        Id = 1,
        FirstName = 2,
        LastName = 3,
        MI = 4,
        PreferredName = 5,
        Tips = 6,
        HomeAddress = 7,
        City = 8,
        StateId = 9,
        ZipCode = 10,
        CountyId = 11,
        Email = 12,
        Birthdate = 13,
        HomePhone = 14,
        OtherPhone = 15,
        OtherPhoneDesc = 16,
        BillingName = 17,
        BillingAddress = 18,
        BillingCity = 19,
        BillingStateId = 20,
        BillingZip = 21,
        SocialSecurity = 22,
        Medicaid = 23,
        InsuranceId = 24,
        DASHPass = 25,
        GenderId = 26,
        RaceId = 27,
        EthnicityId = 28,
        MaritalStatusId = 29,
        LivingStatusId = 30,
        LivingArrangementId = 31,
        PovertyLevelId = 32,
        IntakeDate = 33,
        IntakeStaff = 34,
        ReferredById = 35,
        IfRefferedByOther = 36,
        PetId = 38,
        HouseholdIncome = 39,
        DistrictId = 40,
        EmergencyContactDesc = 42
        //IntakeNotes = 39,
    }
    public enum CounselingRecordFieldIndex
    {
        ClientId = 1,
        Id = 2,
        CaseWorkerId = 3,
        Title20Directions = 4,
        CloseDate = 5,
        OpenDate = 6,
        Tips = 7,
        //StatusId = 4,
    }
    public enum CounselingActivityFieldIndex
    {
        CounselingRecordId = 1,
        Id = 3,
        Enabled = 4,
        Date = 5,
        CounselorId = 6,
        ActivityTypeId = 7,
        ProgramInfoId = 8,
        SubServiceId = 9,
        Note = 10,
        EnteredById = 11,
        EnteredOn = 12,
        Minutes = 13,
    }
    public enum APSRecordFieldIndex
    {
        ClientId = 1,
        Id = 2,
        CaseWorkerId = 3,
        CloseDate = 4,
        OpenDate = 5,
        Tips = 6,
        StatusId = 7,
    }
    public enum StatusHistoryFieldIndex
    {
        Id = 3,
        StatusId = 4,
        Date = 5,
        StatusReasonId = 6,
        EnteredById = 7,
        EnteredOn = 8,
        APSRecordId = 12,
        CounselingRecordId = 18,
        ProgramInfoId = 19,
        SubServiceId = 20,
        //HomeCareRecordId = 2,
        //MealsOnWheelsRecordId = 3,
        //TransportationRecordId = 4,
    }
    #endregion
}
