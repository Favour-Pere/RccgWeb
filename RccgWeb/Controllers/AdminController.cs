using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RccgWeb.Data;
using RccgWeb.Models;
using RccgWeb.Services;
using RccgWeb.Services.Interfaces;
using RccgWeb.ViewModel;
using System.Linq;

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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            return View(users);
        }

        public IActionResult AssignChurch()
        {
            var assignedUserIds = _context.UserChurches.Select(uc => uc.UserId).ToList();
            var assignedChurchIds = _context.UserChurches.Select(uc => uc.ChurchId).ToList();

            var model = new ChurchAssignmentViewModel
            {
                Users = _context.Users
                    .Where(u => !assignedUserIds.Contains(u.Id)) // Fetch unassigned users
                    .Select(u => new SelectListItem { Value = u.Id, Text = u.Email })
                    .ToList(),

                Churches = _context.Zones
                    .Where(z => !assignedChurchIds.Contains(z.ZoneId.ToString()))
                    .Select(z => new SelectListItem { Value = z.ZoneId.ToString(), Text = z.ZoneName })
                    .Concat(_context.Areas
                        .Where(a => !assignedChurchIds.Contains(a.AreaId.ToString()))
                        .Select(a => new SelectListItem { Value = a.AreaId.ToString(), Text = a.AreaName }))
                    .Concat(_context.Parishes
                        .Where(p => !assignedChurchIds.Contains(p.ParishId.ToString()))
                        .Select(p => new SelectListItem { Value = p.ParishId.ToString(), Text = p.ParishName }))
                    .ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AssignChurch(ChurchAssignmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userChurch = new UserChurch
                {
                    UserId = model.UserId,
                    ChurchId = model.ChurchId
                };

                _context.UserChurches.Add(userChurch);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index)); // Redirect to confirmation page
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UnassignChurch()
        {
            var assignedUser = await _context.UserChurches.Select(uc => new SelectListItem
            {
                Value = uc.UserId,
                Text = _context.Users.Where(u => u.Id == uc.UserId).Select(u => u.Email).FirstOrDefault() ?? "Unknown User"
            }).ToListAsync();

            var model = new UnassignedChurchViewModel
            {
                AssignedUsers = assignedUser
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UnassignChurch(UnassignedChurchViewModel model)
        {
            if (string.IsNullOrEmpty(model.SelectedUserId))
            {
                TempData["SuccessMessage"] = "Please select a valid user.";
                return RedirectToAction("UnassignChurch");
            }

            var userChurch = await _context.UserChurches.FirstOrDefaultAsync(uc => uc.UserId == model.SelectedUserId);

            if (userChurch is not null)
            {
                _context.UserChurches.Remove(userChurch);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "User successfully unassigned from church.";
            }
            else
            {
                TempData["SuccessMessage"] = "User is not assigned to any church.";
            }

            return RedirectToAction("UnassignChurch");
        }
    }
}