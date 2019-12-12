using Microsoft.AspNetCore.Mvc;

namespace Blogn.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public ViewResult ServerError()
        {
            return View();
        }
    }
}
