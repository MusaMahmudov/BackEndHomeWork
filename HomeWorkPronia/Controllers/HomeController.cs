using HomeWorkPronia.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace HomeWorkPronia.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var cards = _context.Cards;
            return View(cards);
        }
    }
}
