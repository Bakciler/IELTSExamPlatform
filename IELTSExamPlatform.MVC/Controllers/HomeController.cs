using Microsoft.AspNetCore.Mvc;

namespace IELTSExamPlatform.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
