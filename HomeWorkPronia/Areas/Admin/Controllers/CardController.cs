using HomeWorkPronia.Contexts;
using HomeWorkPronia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWorkPronia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CardController : Controller
    {
        private readonly AppDbContext _context;
        public CardController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var card = _context.Cards;
            return View(card);
        }
        public async Task<IActionResult> Detail(int Id)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(x => x.Id == Id);
            if (card == null) { return NotFound(); }


            return View(card);
        }
        public IActionResult Create()
        {
            if (_context.Cards.Count() < 3)
                return View();

            else 
            {
                return RedirectToAction(nameof(Index));
            }


        }
        [HttpPost]
        public async Task<IActionResult> Create(Card card)
        {

                await _context.AddAsync(card);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           
               

        }
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Cards.Count() > 1)
            {
                var card = await _context.Cards.FirstOrDefaultAsync(s => s.Id == id);
                if (card == null) { return NotFound(); };
                return View(card);

            }
            else
            {
                return RedirectToAction(nameof(Index));
            }




        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(s => s.Id == id);
            if (card == null) { return NotFound(); };

            _context.Cards.Remove(card);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

        }
    }
   
}
