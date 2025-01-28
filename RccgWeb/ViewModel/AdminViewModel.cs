using RccgWeb.DTO;

namespace RccgWeb.ViewModel
{
    public class AdminViewModel
    {
        public List<UserDto> Users { get; set; }

        public List<ChurchDto> Churches { get; set; }
    }
}