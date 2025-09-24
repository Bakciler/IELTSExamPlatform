using Microsoft.AspNetCore.Mvc;

namespace IELTSExamPlatform.MVC.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
