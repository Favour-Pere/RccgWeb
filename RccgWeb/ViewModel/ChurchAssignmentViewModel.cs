using Microsoft.AspNetCore.Mvc.Rendering;

namespace RccgWeb.ViewModel
{
    public class ChurchAssignmentViewModel
    {
        public string UserId { get; set; } = default!;
        public string ChurchId { get; set; } = default!;
        public List<SelectListItem> Users { get; set; } = new(); // List of unassigned users
        public List<SelectListItem> Churches { get; set; } = new();
    }
}