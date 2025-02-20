using RccgWeb.ViewModel;

namespace RccgWeb.Services.Interfaces
{
    public interface IChurchAdminService
    {
        public Task<bool> AssignUserToChurchAsync(ChurchAssignmentViewModel model);

        public Task<bool> RemoveUserFromChurchAsync(string userId);
    }
}