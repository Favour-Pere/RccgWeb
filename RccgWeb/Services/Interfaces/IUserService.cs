namespace RccgWeb.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationBuilder> GetUserByIdAsync(string userId);

        Task<bool> AssignRoleToUserAsync(string userId, string role);

        Task<bool> RemoveRoleFromUserAsync(string userId, string role);
    }
}