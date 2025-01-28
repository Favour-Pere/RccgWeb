using Microsoft.AspNetCore.Mvc;

namespace RccgWeb.Controllers
{
    public class PastorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
