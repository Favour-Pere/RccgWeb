using Microsoft.AspNetCore.Mvc;

namespace RccgWeb.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
