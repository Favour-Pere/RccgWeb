using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RccgWeb.Data;
using RccgWeb.Models;
using RccgWeb.ViewModel;

namespace RccgWeb.Controllers
{
    [Authorize]
    public class ProgramActivityController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProgramActivityController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is  null)
            {
                return RedirectToAction("Login", "Account");
            }

            var userHasChurch = await _context.UserChurches.FirstOrDefaultAsync(uc => uc.UserId == user.Id.ToString());

            if (userHasChurch == null)
            {
                TempData["Message"] = "You need to be assigned to a church before adding activities. Please Contanct an administrator";

                return View("Index");
            }

            var activities = await _context.ProgramActivities
                    .Where(pa => pa.ChurchId == userHasChurch.ChurchId)
                    .OrderByDescending(pa => pa.DateTimeSubmitted) // Assuming there's a CreatedAt field
                    .ToListAsync();

            return View(activities);
        }

        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is not null)
            {
                var userHasChurch = await _context.UserChurches.AnyAsync(uc => uc.UserId == user.Id);

                if (!userHasChurch)
                {
                    TempData["Message"] = "You need to be assigned to a church before adding activities.";

                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProgramActivityViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null)
            {
                return RedirectToAction("Login", "Account");
            }
            var userChurch = await _context.UserChurches.FirstOrDefaultAsync(uc => uc.UserId == user.Id);

            if (userChurch is null)
            {
                TempData["Error"] = "You need to be assigned to a church before adding activities.";
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid activity data. Please check the form and try again.";
                return View(model);
            }

            var newActivity = new ProgramActivity
            {
                ActivityName = model.ActivityName,
                ActivityDescription = model.ActivityDescription,
                DateCreated = model.DateCreated,
                Offering = model.Offering,
                Tithe = model.Tithe,
                Attendance = model.Attendance,
                ActiveWorkers = model.ActiveWorkers,
                PastorInCharge = model.PastorInCharge,
                ChurchId = userChurch.ChurchId
            };

            _context.ProgramActivities.Add(newActivity);

            await _context.SaveChangesAsync();

            TempData["Success"] = "Activity added successfully";

            return RedirectToAction("Index");
        }

    }
}