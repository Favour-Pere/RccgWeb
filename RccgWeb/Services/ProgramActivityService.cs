using Microsoft.EntityFrameworkCore;
using RccgWeb.Data;
using RccgWeb.Models;
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

            return (currentMonthOffering - previousMonthOffering) / previousMonthOffering * 100;
        }

        public async Task<decimal> GetMonthlyOfferingAsync(string churchId, int year, int month)
        {
            return await _context.ProgramActivities.Where(a => a.ChurchId == churchId && a.DateTimeSubmitted.Year == year && a.DateTimeSubmitted.Month == month).SumAsync(a => a.Offering);
        }

        public async Task<Dictionary<string, decimal>> GetMonthlyOfferingBreakdownAsync(string churchId, int year)
        {
            var startDate = new DateTime(year, 1, 1);
            var endDate = new DateTime(year + 1, 1, 1);

            var data = await _context.ProgramActivities
                .Where(a => a.ChurchId == churchId && a.DateTimeSubmitted >= startDate && a.DateTimeSubmitted < endDate)
                .ToListAsync(); // Pull into memory

            return data
                .GroupBy(a => a.DateTimeSubmitted.Month)
                .OrderBy(g => g.Key)
                .ToDictionary(
                    g => new DateTime(year, g.Key, 1).ToString("MMMM"),
                    g => g.Sum(a => a.Offering)
                );
        }

        public async Task<Dictionary<string, decimal>> GetMonthlyTithesBreakdownAsync(string churchId, int year)
        {
            var startDate = new DateTime(year, 1, 1);
            var endDate = new DateTime(year + 1, 1, 1);

            var data = await _context.ProgramActivities
                .Where(a => a.ChurchId == churchId && a.DateTimeSubmitted >= startDate && a.DateTimeSubmitted < endDate).ToListAsync();

            return data.GroupBy(a => a.DateTimeSubmitted.Month).OrderBy(g => g.Key).ToDictionary(g => new DateTime(year, g.Key, 1).ToString("MMMM"), g => g.Sum(a => a.Tithe));
        }

        public async Task<Dictionary<string, int>> GetMonthlyAttendanceBreakdownAsync(string churchId, int year)
        {
            var startDate = new DateTime(year, 1, 1);
            var endDate = new DateTime(year + 1, 1, 1);

            var data = await _context.ProgramActivities
                .Where(a => a.ChurchId == churchId && a.DateTimeSubmitted >= startDate && a.DateTimeSubmitted < endDate).ToListAsync();

            return data.GroupBy(a => a.DateTimeSubmitted.Month).OrderBy(g => g.Key).ToDictionary(g => new DateTime(year, g.Key, 1).ToString("MMMM"), g => g.Sum(a => a.Attendance));
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

        public async Task<PaginatedActivitiesResult> GetPaginatedRecentActivitiesAsync(string churchId, int page, int pageSize)
        {
            var totalActivities = await _context.ProgramActivities.Where(a => a.ChurchId == churchId).CountAsync();

            var totalPages = (int)Math.Ceiling(totalActivities / (double)pageSize);

            var activities = await _context.ProgramActivities.Where(a => a.ChurchId == churchId).OrderByDescending(a => a.DateTimeSubmitted).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedActivitiesResult
            {
                Activities = activities,
                TotalPages = totalPages,
                CurrentPage = page
            };
        }
    }

    public class PaginatedActivitiesResult
    {
        public List<ProgramActivity> Activities { get; set; } = new();
        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }
    }
}