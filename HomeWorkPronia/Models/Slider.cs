using System.ComponentModel.DataAnnotations;

namespace HomeWorkPronia.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [Required,MaxLength(100)]
        public string Suptitle { get; set; }
        [Required, MaxLength(150)]

        public string Title { get; set; }
        [Required, MaxLength(300)]

        public string Description { get; set; }

        public string Image { get; set; }

    }
}
