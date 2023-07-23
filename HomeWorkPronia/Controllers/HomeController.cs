using HomeWorkPronia.Contexts;
using HomeWorkPronia.ViewModels;
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
            var sliders = _context.Sliders;
            HomeViewModel homeviewmodel = new HomeViewModel();
            homeviewmodel.Cards = cards;
            homeviewmodel.Sliders = sliders;
            return View(homeviewmodel);
        }
    }
}
