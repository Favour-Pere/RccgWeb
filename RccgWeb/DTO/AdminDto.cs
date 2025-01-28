using RccgWeb.Models;

namespace RccgWeb.DTO
{
    public class AdminDto(UserModel user)
    {
        public string? ID { get; set; } = user.Id;

        public string? UserName { get; set; } = user.UserName;
    }
}