using RccgWeb.Models;

namespace RccgWeb.ViewModel
{
    public class ActivityViewModel
    {
        public string ChurchName { get; set; } // Name of the assigned church
        public string Location { get; set; } // Church Location
        public DateTime? DateCreated { get; set; } // Date the Church was created
        public List<ProgramActivity> Activities { get; set; } // List of activities
    }
}