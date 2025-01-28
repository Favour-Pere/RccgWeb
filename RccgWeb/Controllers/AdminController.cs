using Microsoft.AspNetCore.Mvc;

namespace RccgWeb.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllChurchesAsync()
        {
            return View();
        }

        public IActionResult GetChurchByID()
        {
            return View();
        }

        public IActionResult CreateChurch()
        {
            return View();
        }

        public IActionResult UpdateChurch()
        {
            return View();
        }

        public IActionResult DeleteChurch()
        {
            return View();
        }

        public IActionResult ChurchDetails()
        {
            return View();
        }

        public async Task<IActionResult> GetAllPastorsAsync()
        {
            return View();
        }

        public async Task<IActionResult> GetPastorByID()
        {
            return View();
        }

        public IActionResult AddPastor()
        {
            return View();
        }

        public IActionResult EditPastor()
        {
            return View();
        }

        public IActionResult DeletePastor()
        {
            return View();
        }

        public IActionResult PastorDetails()
        {
            return View();
        }
    }
}