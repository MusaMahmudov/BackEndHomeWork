using HomeWorkPronia.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace HomeWorkPronia.Models
{
    public class Slider : BaseEntity
    {
       
        [Required,MaxLength(100)]
        public string Suptitle { get; set; }
        [Required, MaxLength(150)]

        public string Title { get; set; }
        [Required, MaxLength(300)]

        public string Description { get; set; }

        public string Image { get; set; }

    }
}
