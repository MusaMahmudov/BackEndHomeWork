using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HomeWorkPronia.Models.Common;

namespace HomeWorkPronia.Areas.Admin.ViewModels.ProductViewModels
{
    public class UpdateProductViewModel 
    {
        public string Name { get; set; }
        [Required, Range(1, 5)]
        public int Rating { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime UpdateTime { get; set; }

    }
}
