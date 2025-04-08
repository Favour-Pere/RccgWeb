using RccgWeb.Models;

namespace RccgWeb.Services.Interfaces
{
    public interface IProgramActivityService
    {
        Task<decimal> GetTotalOfferingAsync(string churchId);

        Task<decimal> GetTotalTitheAsync(string churchId);

        Task<int> GetTotalAttendanceAsync(string churchId);

        Task<int> GetActiveWorkersAsync(string churchId);

        Task<Dictionary<string, decimal>> GetMonthlyOfferingBreakdownAsync(string churchId, int year);

        Task<decimal> GetGrowthRateAsync(string churchId);
    }
}