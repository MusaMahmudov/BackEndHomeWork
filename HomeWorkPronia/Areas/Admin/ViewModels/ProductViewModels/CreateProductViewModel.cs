using HomeWorkPronia.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeWorkPronia.Areas.Admin.ViewModels.ProductViewModels
{
    public class CreateProductViewModel
    {
        public string Name { get; set; }
        [Required,Range(1,5)]
        public int Rating { get; set; }
        public string Description {  get; set; }
        public IFormFile Image { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price {  get; set; }
        public int CategoryId { get; set; }

    }
}
