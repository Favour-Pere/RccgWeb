using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RccgWeb.Data;
using RccgWeb.Models;
using RccgWeb.Services;
using RccgWeb.Services.Interfaces;
using RccgWeb.ViewModel;

namespace RccgWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IChurchAdminService _churchAdminService;

        public AdminController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IChurchAdminService churchAdminService)
        {
            _userManager = userManager;
            _context = context;
            _churchAdminService = churchAdminService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            return View(users);
        }

        public async Task<IActionResult> UserList()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        public IActionResult AssignUser()
        {
            return View(new ChurchAssignmentViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AssignChurch(string userId, string churchId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) return NotFound();

            var existingAssignment = await _context.UserChurches.FirstOrDefaultAsync(uc => uc.UserId == userId);

            if (existingAssignment != null)
            {
                existingAssignment.ChurchId = churchId;
            }
            else
            {
                _context.UserChurches.Add(new UserChurch
                {
                    UserId = userId,
                    ChurchId = churchId
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UnassignChurch(string userId)
        {
            var userChurch = await _context.UserChurches.FirstOrDefaultAsync(uc => uc.UserId == userId);
            if (userChurch == null) return NotFound();

            _context.UserChurches.Remove(userChurch);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // this section might not be needed

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignUser(ChurchAssignmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var success = await _churchAdminService.AssignUserToChurchAsync(model);

                if (success)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Error assigning user.");
            }

            return View(model);
        }

        public async Task<IActionResult> RemoveUser(string userId)
        {
            var success = await _churchAdminService.RemoveUserFromChurchAsync(userId);

            return success ? RedirectToAction("Index") : BadRequest("Error removing user.");
        }
    }
}