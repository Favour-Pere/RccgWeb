using Microsoft.AspNetCore.Identity;

namespace RccgWeb.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string ChurchId { get; set; } = String.Empty;

        public Guid? ZoneId { get; set; }

        public Guid? AreaId { get; set; }

        public Guid? ParishId { get; set; }

        public bool IsAssigned => !string.IsNullOrEmpty(ChurchId);

        public virtual Zone? Zone { get; set; }
        public virtual Area? Area { get; set; }
        public virtual Parish? Parish { get; set; }
    }
}