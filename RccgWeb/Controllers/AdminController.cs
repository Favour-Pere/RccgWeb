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
using System.Collections.Immutable;
using System.Linq;

namespace RccgWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IChurchAdminService _churchAdminService;
        private readonly IProgramActivityService _programActivityService;
        private readonly IChurchService _churchService;

        public AdminController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IChurchAdminService churchAdminService, IProgramActivityService programActivityService, IChurchService churchService)
        {
            _userManager = userManager;
            _context = context;
            _churchAdminService = churchAdminService;
            _programActivityService = programActivityService;
            _churchService = churchService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null)
            {
                return RedirectToAction("Login", "Account");
            }

            var userHasChurch = await _context.UserChurches.FirstOrDefaultAsync(uc => uc.UserId == user.Id.ToString());

            if (userHasChurch == null)
            {
                TempData["Message"] = "You need to be assigned to a church before adding activities. Please Contanct an administrator";

                return View("Index");
            }

            var activities = await _context.ProgramActivities.ToListAsync();

            var model = new AdminDashboardViewModel
            {
                TotalUsers = await _context.Users.CountAsync(),
                Zones = await _context.Zones.ToListAsync(),
                Areas = await _context.Areas.ToListAsync(),
                Parishes = await _context.Parishes.ToListAsync(),
                TotalAttendance = await _context.ProgramActivities.SumAsync(a => a.Attendance),
                TotalOfferings = await _context.ProgramActivities.SumAsync(a => a.Offering),
                ActiveWorkers = await _context.ProgramActivities.SumAsync(a => a.ActiveWorkers),
                TotalTithe = await _context.ProgramActivities.SumAsync(a => a.Tithe),
                RecentGrowthRate = await CalculateGrowthRate(),
                TopPerformingChurch = await GetTopPerformingChurch(),
                RecentActivities = activities
            };
            return View(model);
        }

        private async Task<double> CalculateGrowthRate()
        {
            var currentMonth = DateTime.Now.Month;
            var previousMonth = currentMonth == 1 ? 12 : currentMonth - 1;

            var currentAttendance = await _context.ProgramActivities
                .Where(a => a.DateTimeSubmitted.Month == currentMonth)
                .SumAsync(a => a.Attendance);

            var previousAttendance = await _context.ProgramActivities
                .Where(a => a.DateTimeSubmitted.Month == previousMonth)
                .SumAsync(a => a.Attendance);

            if (previousAttendance == 0)
                return 0; // Avoid division by zero

            return ((double)(currentAttendance - previousAttendance) / previousAttendance) * 100;
        }

        private async Task<string> GetTopPerformingChurch()
        {
            // Get the ChurchId of the top-performing church
            var topChurchId = await _context.ProgramActivities
                .GroupBy(a => a.ChurchId)
                .OrderByDescending(g => g.Sum(a => a.Attendance))
                .Select(g => g.Key)
                .FirstOrDefaultAsync();

            if (topChurchId == null)
                return "No Data";

            // Fetch Church details from Zones, Areas, or Parishes
            var topChurchDetails = await _context.Zones
                .Where(z => z.ChurchId == topChurchId)
                .Select(z => new { Name = z.ZoneName, Location = z.Location })
                .Union(_context.Areas.Where(a => a.ChurchId == topChurchId)
                    .Select(a => new { Name = a.AreaName, Location = a.Location }))
                .Union(_context.Parishes.Where(p => p.ChurchId == topChurchId)
                    .Select(p => new { Name = p.ParishName, Location = p.Location }))
                .FirstOrDefaultAsync();

            if (topChurchDetails == null)
                return "Unknown Church";

            // Return the church name and location
            return $"{topChurchDetails.Name} ({topChurchDetails.Location})";
        }

        public async Task<IActionResult> UserList()
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
                    .Select(u => new SelectListItem { Value = u.Id, Text = $"{u.Email} {u.FirstName} {u.LastName}" })
                    .ToList(),

                Churches = _context.Zones
                    .Where(z => !assignedChurchIds.Contains(z.ChurchId))
                    .Select(z => new SelectListItem { Value = z.ChurchId, Text = z.ZoneName })
                    .Concat(_context.Areas
                        .Where(a => !assignedChurchIds.Contains(a.ChurchId))
                        .Select(a => new SelectListItem { Value = a.ChurchId, Text = a.AreaName }))
                    .Concat(_context.Parishes
                        .Where(p => !assignedChurchIds.Contains(p.ChurchId))
                        .Select(p => new SelectListItem { Value = p.ChurchId, Text = p.ParishName }))
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

        public async Task<IActionResult> AllProgramActivity()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null)
            {
                return RedirectToAction("Login", "Account");
            }

            var userHasChurch = await _context.UserChurches.FirstOrDefaultAsync(uc => uc.UserId == user.Id.ToString());

            if (userHasChurch == null)
            {
                TempData["Message"] = "You need to be assigned to a church before adding activities. Please Contanct an administrator";

                return View("Index");
            }

            var activities = await _context.ProgramActivities.ToListAsync();

            return View(activities);
        }

        [HttpGet]
        public async Task<IActionResult> ChurchDetails(string id, int page = 1)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Church ID is required.");
            }

            var currentYear = DateTime.Now.Year;

            var currentMonth = DateTime.Now.Month;

            var churchName = await _churchService.GetChurchNameAsync(id);

            var pagedResult = await _programActivityService.GetPaginatedRecentActivitiesAsync(id, page, 10)
            var stats = new ChurchStatsViewModel
            {
                ChurchId = id,
                ChurchName = churchName ?? "Unknown Church",
                Year = currentYear,
                Month = currentMonth,

                MonthlyOfferings = await _programActivityService.GetMonthlyOfferingBreakdownAsync(id, currentYear),
                MonthlyTithes = await _programActivityService.GetMonthlyTithesBreakdownAsync(id, currentYear),
                MonthlyAttendance = await _programActivityService.GetMonthlyAttendanceBreakdownAsync(id, currentYear),
                RecentActivities = pagedResult.Activities,
                CurrentPage = pagedResult.CurrentPage,
                TotalPages = pagedResult.TotalPages
            };

            return View(stats); // Return the view with the stats model
        }
    }
}