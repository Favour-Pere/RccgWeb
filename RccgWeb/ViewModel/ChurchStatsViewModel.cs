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
}