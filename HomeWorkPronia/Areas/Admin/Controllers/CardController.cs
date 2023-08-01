using HomeWorkPronia.Areas.Admin.ViewModels.CardViewModels;
using HomeWorkPronia.Contexts;
using HomeWorkPronia.Exceptions;
using HomeWorkPronia.Models;
using HomeWorkPronia.Services.Interfaces;
using HomeWorkPronia.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HomeWorkPronia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CardController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;
        public CardController(AppDbContext context,IWebHostEnvironment webHostEnvironment,IFileService fileService)
        {
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var card = await _context.Cards.AsNoTracking().ToArrayAsync();
            return View(card);
        }
        public async Task<IActionResult> Detail(int Id)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(x => x.Id == Id);
            if (card == null) { return NotFound(); }


            return View(card);
        }
        public async Task<IActionResult> Create()
        {
            if (await _context.Cards.CountAsync() == 3)
            {
                return RedirectToAction(nameof(Index));
            }
            else { return View(); }


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCardViewModel createCardViewModel)
        {
			if (await _context.Cards.CountAsync() == 3)
				return BadRequest();


            if (!ModelState.IsValid)
                return View();


            //if(!createCardViewModel.Image.CheckFileType("image/"))
            //{
            //    ModelState.AddModelError("Image", "shekil daxil ele");
            //    return View();
            //}
            //if (!createCardViewModel.Image.CheckFileSize(1000))
            //{
            //    ModelState.AddModelError("Image", "cox yer tutur");
            //    return View();
            //}

            //string filename = $"{Guid.NewGuid}-{createCardViewModel.Image.FileName}";
            //string path = Path.Combine(_webHostEnvironment.WebRootPath,"assets","images","website-images",filename);

            //using (FileStream fileStream = new FileStream(path, FileMode.Create))
            //{
            //    await createCardViewModel.Image.CopyToAsync(fileStream);

            //}
            string fileName = string.Empty;
            try
            {
                 fileName = await _fileService.CreateFileAsync(createCardViewModel.Image, Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "website-images"));

            }
            catch(FileTypeException ex)
            {
                ModelState.AddModelError("Image", "shekil daxil ele");
                return View();
            }
            catch(FileSizeException ex) 
            {
                ModelState.AddModelError("Image", "cox yer tutur");
                return View();
            }
            catch 
            (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
            Card card = new Card
            {
               Image = fileName,
               Title = createCardViewModel.Title,
               Description = createCardViewModel.Description,

            };
            


            await _context.AddAsync(card);

            await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           
               

        }

      

        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Cards.Count() == 1)
                return BadRequest();


            
            var card = await _context.Cards.FirstOrDefaultAsync(s => s.Id == id);

                if (card == null) { return NotFound(); };

            DeleteCardViewModel deleteCardViewModel = new DeleteCardViewModel
            {
                Image = card.Image,
                Title = card.Title,
                Description = card.Description,

            };
                return View(deleteCardViewModel);
            
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCard(int id)
        {
			if (_context.Cards.Count() == 1)
				return BadRequest();

            var card = await _context.Cards.FirstOrDefaultAsync(s => s.Id == id);
            if (card == null) { return NotFound(); };

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "website-images", card.Image);
            _fileService.DeleteFile(path);
            //if(System.IO.File.Exists(path))
            //{
            //    System.IO.File.Delete(path);
            //}


            card.IsDeleted = true;
         await  _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Update(int Id)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(s => s.Id == Id);
            if(card == null) { return NotFound(); };
           
            UpdateCardViewModel updateCardViewModel = new UpdateCardViewModel
            {
                Id = card.Id,
                Description = card.Description,
                Title = card.Title
               
            };
            return View(updateCardViewModel);

        }
        [HttpPost]
        [ActionName("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCard(int id, UpdateCardViewModel updateCardViewModel)
        {
            if (!ModelState.IsValid)
                return View();


            var card =await _context.Cards.FirstOrDefaultAsync(s=>s.Id == id);
            if (card == null) { return NotFound();}

            if(updateCardViewModel.Image is not null)
            {
                try
                {
                    string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "website-images", card.Image);
                    _fileService.DeleteFile(path);
                    string fileName = await _fileService.CreateFileAsync(updateCardViewModel.Image, Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "website-images"));
                    card.Image = fileName;
                }
                catch (FileTypeException ex)
                {
                    ModelState.AddModelError("Image", "shekil daxil ele");
                    return View();
                }
                catch (FileSizeException ex)
                {
                    ModelState.AddModelError("Image", "cox yer tutur");
                    return View();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Image", $"{ex.Message}");
                    return View();
                }



            }

            card.Title = updateCardViewModel.Title;
            card.Description = updateCardViewModel.Description;



            _context.Cards.Update(card);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }
   
}
