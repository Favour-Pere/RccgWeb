using Microsoft.AspNetCore.Identity;

namespace RccgWeb.Models
{
    public class User : IdentityUser
    {
        public string ChurchId { get; set; } = String.Empty;
    }
}