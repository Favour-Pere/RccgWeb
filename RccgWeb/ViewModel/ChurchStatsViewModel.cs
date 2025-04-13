using RccgWeb.Models;
using System;

namespace RccgWeb.ViewModel;

public class ChurchStatsViewModel
{
    public string ChurchId { get; set; } = string.Empty;
    public string ChurchName { get; set; } = string.Empty;

    public Dictionary<string, decimal> MonthlyOfferings { get; set; } = new();
    public Dictionary<string, decimal> MonthlyTithes { get; set; } = new();
    public Dictionary<string, int> MonthlyAttendance { get; set; } = new();

    public int Year { get; set; }
    public int Month { get; set; }

    public List<ProgramActivity> RecentActivities { get; set; } = new();
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }

    public string PastorName { get; set; } = string.Empty;
    public string PastorPhone { get; set; } = string.Empty;

    public string PastorEmail { get; set; } = string.Empty;
}