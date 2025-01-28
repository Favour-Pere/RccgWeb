using Microsoft.AspNetCore.Mvc;
using RccgWeb.DTO;
using RccgWeb.Models;

namespace RccgWeb.Services.Interfaces
{
    public interface IAdminService
    {
        public Task<AdminDto> GetAuthenticatedAdmin();

        public Task<UserDto> GetUserDetails(string userId);

        public int GetUsersCount();

        public Task<List<PastorDto>> GetPastors();

        public Task<List<ChurchDto>> GetChurches();

        public Task<List<UserDto>> GetUsers();

        public Task<IActionResult> AddPastor(UserModel? user);
    }
}