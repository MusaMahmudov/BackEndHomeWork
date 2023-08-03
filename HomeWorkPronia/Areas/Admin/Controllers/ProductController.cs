using AutoMapper;
using HomeWorkPronia.Areas.Admin.ViewModels.ProductViewModels;
using HomeWorkPronia.Contexts;
using HomeWorkPronia.Exceptions;
using HomeWorkPronia.Models;
using HomeWorkPronia.Services.Implementations;
using HomeWorkPronia.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HomeWorkPronia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        public ProductController(AppDbContext context,IWebHostEnvironment webHostEnvironment,IFileService fileService,IMapper mapper)
        {
            _mapper = mapper ;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var products =await _context.Products.AsNoTracking().ToListAsync();

            return View(products);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(),"Id","Name");
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel createProductViewModel)
        {
            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");

            if (!ModelState.IsValid)
            {
                return View();
            }


            string FileName = string.Empty;
            var product = _mapper.Map<Product>(createProductViewModel);

            try
            {
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "website-images");
                 product.Image = await _fileService.CreateFileAsync(createProductViewModel.Image, path);
               

            }
            catch (FileTypeException ex)
            {
                ModelState.AddModelError("Image", "Shekil deyil");
                return View();

            }
            catch(FileSizeException ex)
            {
                ModelState.AddModelError("Image", "Hecim Coxdu");
                return View();

            }
            //Product product = new Product
            //{
            //    Name = createProductViewModel.Name,
            //    Rating = createProductViewModel.Rating,
            //    Description = createProductViewModel.Description,
            //    CategoryId = createProductViewModel.CategoryId,
            //    Image = FileName,
            //    Price = createProductViewModel.Price,


            //};



            await _context.Products.AddAsync(product);
           await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));   



        }
        public async Task<IActionResult> Detail(int Id)
        {
            Product? product =await _context.Products.Include(p=>p.Category).FirstOrDefaultAsync(p=>p.Id == Id);
            if(product is null) 
            { 
            return View();
            }
            if(!ModelState.IsValid)
            {
                return View();
            }

            //DetailProductViewModel detailProductViewModel = new DetailProductViewModel()
            //{
            //    Name = product.Name,
            //    Rating = product.Rating,
            //    Description = product.Description,
            //    CategoryName = product.Category.Name,
            //    CreatedBy = product.CreatedBy,
            //    UpdatedBy = product.UpdatedBy,
            //    UpdateTime = product.UpdateTime,
            //    CreatedDate = product.CreatedDate,  
            //    Image = product.Image,
            //    Price = product.Price,
            //};
            DetailProductViewModel detailProductViewModel = _mapper.Map<DetailProductViewModel>(product);
            return View(detailProductViewModel);



        }
        public async Task<IActionResult> Update(int Id)
        {
            var product =await _context.Products.FirstOrDefaultAsync(p => p.Id == Id);
                if(product is null)
                return NotFound();


            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");




         

            UpdateProductViewModel updateProductViewModel = _mapper.Map<UpdateProductViewModel>(product);

            return View(updateProductViewModel);
            
               
            
              


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateProductViewModel updateProductViewModel,int Id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p=>p.Id == Id);

            if (product is null)
                return NotFound();
            string FileName = product.Name;
            try
            {
                if(updateProductViewModel.Image is not null)
                {
                    string path = Path.Combine(_webHostEnvironment.WebRootPath,"assets","images","website-images",product.Image);

                    _fileService.DeleteFile(path);
                    FileName = await _fileService.CreateFileAsync(updateProductViewModel.Image, Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "website-images"));
                    product.Image = FileName;

                }


            }
            catch (FileTypeException ex)
            {
                ModelState.AddModelError("Image","Shekil deyil");
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError("Image", "Size Coxdur");
                return View();

            }
              _mapper.Map(updateProductViewModel, product);
            product.Image = FileName;






            _context.Products.Update(product);
            await _context.SaveChangesAsync(); 
            

            return RedirectToAction(nameof(Index));
        }

    }
}
