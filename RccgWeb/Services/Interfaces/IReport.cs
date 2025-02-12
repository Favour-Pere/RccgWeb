namespace RccgWeb.Services.Interfaces
{
    public interface IReport
    {
        Task<byte[]> GenerateMonthlyActivityReportAsync(string churchId, DateTime month);

        Task<byte[]> GenerateAttendanceReportAsync(string churchId, DateTime startDate, DateTime endDate);

        Task<byte[]> GenerateOfferingReportAsync(string churchId, DateTime startDate, DateTime endDate);
    }
}