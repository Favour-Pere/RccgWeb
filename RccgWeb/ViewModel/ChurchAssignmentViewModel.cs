namespace RccgWeb.ViewModel
{
    public class ChurchAssignmentViewModel
    {
        public string UserId { get; set; } = default!;

        public string ChurchId { get; set; } = default!;

        public Guid? ZoneId { get; set; }

        public Guid? AreaId { get; set; }

        public Guid? ParishId { get; set; }
    }
}