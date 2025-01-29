using Microsoft.AspNetCore.Mvc;
using RccgWeb.DTO;

namespace RccgWeb.Services.Interfaces
{
    public interface IPastorService
    {
        public Task<UserDto> GetUserDetails();

        public Task<PastorDto> GetPastor();
    }
}