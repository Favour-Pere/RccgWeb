using Microsoft.AspNetCore.Mvc.Rendering;

namespace RccgWeb.ViewModel
{
    public class ChurchAssignmentViewModel
    {
        public string UserId { get; set; } = default!;

        public string ChurchId { get; set; } = default!;

        public List<SelectListItem> Churches { get; set; } = new();
        //public Guid? ZoneId { get; set; }

        //public Guid? AreaId { get; set; }

        //public Guid? ParishId { get; set; }
    }
}