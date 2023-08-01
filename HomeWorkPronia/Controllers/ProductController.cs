using Microsoft.AspNetCore.Mvc;

namespace HomeWorkPronia.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
