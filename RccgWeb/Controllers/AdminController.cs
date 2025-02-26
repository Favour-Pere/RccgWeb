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

        //[HttpGet]
        //public async Task<IActionResult> AssignChurch(string userId)
        //{
        //    if (string.IsNullOrEmpty(userId))
        //    {
        //        return NotFound();
        //    }

        //    var user = await _userManager.FindByIdAsync(userId);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    var assignedChurchIds = _context.UserChurches.Select(uc => uc.ChurchId).ToList();

        //    var unassignedZones = _context.Zones.Where(z => !assignedChurchIds.Contains(z.ZoneId.ToString())).Select(z => new SelectListItem
        //    {
        //        Value = z.ZoneId.ToString(),
        //        Text = z.ZoneName
        //    }).ToList();

        //    var unassignedAreas = _context.Areas.Where(a => !assignedChurchIds.Contains(a.AreaId.ToString())).Select(a => new SelectListItem
        //    {
        //        Value = a.AreaId.ToString(),
        //        Text = a.AreaName
        //    }).ToList();

        //    var unassignedParishes = _context.Parishes.Where(p => !assignedChurchIds.Contains(p.ParishId.ToString())).Select(p => new SelectListItem
        //    {
        //        Value = p.ParishId.ToString(),
        //        Text = p.ParishName
        //    }).ToList();

        //    var unassignedChurches = unassignedZones.Concat(unassignedAreas).Concat(unassignedParishes).ToList();

        //    var model = new ChurchAssignmentViewModel
        //    {
        //        UserId = userId,
        //        Churches = unassignedChurches
        //    };

        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> AssignChurch(ChurchAssignmentViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByIdAsync(model.UserId);
        //        if (user == null)
        //        {
        //            return NotFound();
        //        }

        //        // Assign user to the church
        //        var userChurch = new UserChurch
        //        {
        //            UserId = model.UserId,
        //            ChurchId = model.ChurchId
        //        };

        //        _context.UserChurches.Add(userChurch);
        //        await _context.SaveChangesAsync();

        //        return RedirectToAction("Index", "Admin"); // Redirect to admin dashboard
        //    }

        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> AssignChurch(ChurchAssignmentViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var userChurch = new UserChurch
        //        {
        //            UserId = model.UserId,
        //            ChurchId = model.ChurchId,
        //        };

        //        _context.UserChurches.Add(userChurch);
        //        await _context.SaveChangesAsync();

        //        return RedirectToAction("AssignChurch");
        //    }

        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> AssignChurch(string userId, string churchId)
        //{
        //    var user = await _userManager.FindByIdAsync(userId);

        //    if (user == null) return NotFound();

        //    var existingAssignment = await _context.UserChurches.FirstOrDefaultAsync(uc => uc.UserId == userId);

        //    if (existingAssignment != null)
        //    {
        //        existingAssignment.ChurchId = churchId;
        //    }
        //    else
        //    {
        //        _context.UserChurches.Add(new UserChurch
        //        {
        //            UserId = userId,
        //            ChurchId = churchId
        //        });
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        public async Task<IActionResult> UnassignChurch(string userId)
        {
            var userChurch = await _context.UserChurches.FirstOrDefaultAsync(uc => uc.UserId == userId);
            if (userChurch == null) return NotFound();

            _context.UserChurches.Remove(userChurch);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}