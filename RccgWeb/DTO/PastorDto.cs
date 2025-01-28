using RccgWeb.Models;

namespace RccgWeb.DTO
{
    public class PastorDto(Pastor pastor)
    {
        public string? ID { get; set; } = pastor.ID.ToString();

        public UserDto? PastorDetails { get; set; } = new(pastor.User);
    }
}