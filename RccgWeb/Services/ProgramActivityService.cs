using Microsoft.EntityFrameworkCore;
using RccgWeb.Data;
using RccgWeb.Services.Interfaces;

namespace RccgWeb.Services
{
    public class ProgramActivityService : IProgramActivityService
    {
        private readonly ApplicationDbContext _context;

        public ProgramActivityService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetActiveWorkersAsync(string churchId)
        {
            return await _context.ProgramActivities.Where(a => a.ChurchId == churchId).SumAsync(a => a.ActiveWorkers);
        }

        public async Task<decimal> GetGrowthRateAsync(string churchId)
        {
            var currentMonth = DateTime.Now.Month;
            var previousMonth = currentMonth == 1 ? 12 : currentMonth - 1;
            var year = DateTime.Now.Year;

            var currentMonthOffering = await GetMonthlyOfferingAsync(churchId, year, currentMonth);

            var previousMonthOffering = await GetMonthlyOfferingAsync(churchId, year, previousMonth);

            if (previousMonthOffering == 0)
            {
                return 0;
            }

            return ((currentMonthOffering - previousMonthOffering) / previousMonthOffering) * 100;
        }

        public async Task<decimal> GetMonthlyOfferingAsync(string churchId, int year, int month)
        {
            return await _context.ProgramActivities.Where(a => a.ChurchId == churchId && a.DateTimeSubmitted.Year == year && a.DateTimeSubmitted.Month == month).SumAsync(a => a.Offering);
        }

        public async Task<int> GetTotalAttendanceAsync(string churchId)
        {
            return await _context.ProgramActivities.Where(a => a.ChurchId == churchId).SumAsync(a => a.Attendance);
        }

        public async Task<decimal> GetTotalOfferingAsync(string churchId)
        {
            return await _context.ProgramActivities.Where(a => a.ChurchId == churchId).SumAsync(a => a.Offering);
        }

        public async Task<decimal> GetTotalTitheAsync(string churchId)
        {
            return await _context.ProgramActivities.Where(a => a.ChurchId == churchId).SumAsync(a => a.Tithe);
        }
    }
}