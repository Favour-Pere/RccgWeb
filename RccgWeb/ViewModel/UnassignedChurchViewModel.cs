using Microsoft.AspNetCore.Mvc.Rendering;

namespace RccgWeb.ViewModel
{
    public class UnassignedChurchViewModel
    {
        public string SelectedUserId { get; set; } = string.Empty;

        public List<SelectListItem> AssignedUsers { get; set; } = new();
    }
}