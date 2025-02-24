using Microsoft.AspNetCore.Identity;
using RccgWeb.Data;
using RccgWeb.Models;
using RccgWeb.Services.Interfaces;
using RccgWeb.ViewModel;

namespace RccgWeb.Services
{
    public class ChurchAdminService : IChurchAdminService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChurchAdminService(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<bool> AssignUserToChurchAsync(ChurchAssignmentViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null) return false;

            //user.ParishId = model.ParishId;
            //user.AreaId = model.AreaId;
            //user.ZoneId = model.ZoneId;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<bool> RemoveUserFromChurchAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) return false;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }
    }
}