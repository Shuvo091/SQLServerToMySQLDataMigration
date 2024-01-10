using System.ComponentModel.DataAnnotations;

namespace FSSRC_DataMigration.DestinationEntity.Enums
{
    public enum ReportEnum
    {
        ClientAddressAndPhoneListReport = 1,
        ClientFullDetailReport = 2,
        ClientCaseNotesReport = 3,
        ClientActivityReport = 4,
        MergedClientStatisticsReport = 5,
        OneWayTransportationSummaryReport = 6,
        CompleteTransportationSummaryReport = 7,
        TransportationActivityDetailsReport = 8,
        ClientMasterScheduleReport = 9,
        ClientActualScheduleReport = 10,
        ClientExpenseMileageReport = 11,
        StaffCaseNotesReport = 12,
        StaffInServiceReport = 13,
        ClientFundSummary = 14,
        PayrollStaffSummary = 15,
        PayrollStaffSignatureSheet = 16,
        EmployeePayrollWithClientBillingBreakdownDetailByAllFunds = 17,
        ClientBillingWithExpensesSummaryPrivatePay = 18,
        StaffStatisticsReport = 19,
        CaseLoadReport = 21,
        OpenClosedPerPeriod = 22,
        IndependentLivingReport = 23,
        DirectServiceHoursReport = 25,
        IncomeReport = 26,
        ServiceHoursPerPeriod = 27,
        StaffHiredReport = 28,
        ActivityTypeReport = 30,
        StaffActualScheduleReport = 31,
        StaffMasterScheduleReport = 32,
        PayrollAccountingFundSummary = 33
    }

    public enum ReportParamEnum
    {
        Service = 1,
        Status = 2,
        StartDate = 3,
        EndDate = 4,
        District = 5,
        City = 6,
        Fund = 7,
        EmergencyContactInfo = 8,
        SubService = 9,
        Program = 10,
        PeriodStatus = 11,
        Billing = 17,
        Staff = 19,
        Mode = 20,
        Reason = 21,
        Ride = 22,
        LateRequest = 23,
        Problems = 24,
        Classification = 25,
        HireMonth = 26,
        HireYear = 27,
        Client = 28,
        ActiveDuring = 29
    }

    public enum PeriodStatusEnum
    {
        [Display(Name = "Carryover from Before the Period")] CarryoverFromBeforeThePeriod = 1,
        Returned = 2,
        [Display(Name = "First Timer")] FirstTimer = 3,
        Closed = 4,
        Hold = 5
    }

    public enum ActiveDuringEnum
    {
        Both = 1,
        Yes = 2,
        No = 3
    }
}
