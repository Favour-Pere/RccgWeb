using Microsoft.AspNetCore.Mvc;

namespace RccgWeb.Controllers
{
    public class ChurchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddWorkers()
        {
            return View();
        }

        public IActionResult DeleteWorker()
        {
            return View();
        }

        public IActionResult UpdateWorker()
        {
            return View();
        }

        public IActionResult GetProgramById()
        {
            return View();
        }

        public IActionResult GetAllProgramsAsync()
        {
            return View();
        }

        public IActionResult CreateProgram()
        {
            return View();
        }

        public IActionResult EditProgram()
        {
            return View();
        }

        public IActionResult AddOffering()
        {
            return View();
        }

        public IActionResult AddTithe()
        {
            return View();
        }
    }
}