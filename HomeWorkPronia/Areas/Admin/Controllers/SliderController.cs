using HomeWorkPronia.Contexts;
using HomeWorkPronia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWorkPronia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        public SliderController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var slider = _context.Sliders;

            return View(slider);
        }
        public async Task<IActionResult> Detail(int Id)
        {
            var slider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == Id);
            if (slider == null) { return NotFound(); }


            return View(slider);
        }
        public IActionResult Create()
        {

            return View();


        }
        [HttpPost]
        public async Task<IActionResult> Create(Slider slider)
        {
           await  _context.AddAsync(slider);
           await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int id)
        {
            var slider = await _context.Sliders.FirstOrDefaultAsync(s=> s.Id == id);
            if (slider == null) { return NotFound(); };

            return View(slider);

        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteSlider(int id)
        {
            var slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider == null) { return NotFound(); };

            _context.Sliders.Remove(slider);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

        }



    }
}
