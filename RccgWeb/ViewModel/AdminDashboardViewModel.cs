using RccgWeb.Models;

namespace RccgWeb.ViewModel
{
    public class AdminDashboardViewModel
    {
        public int TotalUsers { get; set; }
        public List<Zone> Zones { get; set; } = new List<Zone>();
        public List<Area> Areas { get; set; } = new List<Area>();
        public List<Parish> Parishes { get; set; } = new List<Parish>();

        public List<ProgramActivity> RecentActivities { get; set; } = new List<ProgramActivity>();
        public int TotalAttendance { get; set; }
        public decimal TotalOfferings { get; set; }

        public decimal TotalTithe { get; set; }

        public double RecentGrowthRate { get; set; }
        public int ActiveWorkers { get; set; }
        public string TopPerformingChurch { get; set; }
    }
}