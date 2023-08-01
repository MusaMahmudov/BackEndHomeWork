using HomeWorkPronia.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HomeWorkPronia.Areas.Admin.ViewModels.ProductViewModels
{
    public class DetailProductViewModel
    {
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }
        [Required, MaxLength(120)]

        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }

        public string CategoryName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdatedBy { get; set; }
    }
}
