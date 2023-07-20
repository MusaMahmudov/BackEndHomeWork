using System.ComponentModel.DataAnnotations;

namespace HomeWorkPronia.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Image { get; set; }
        [Required, MaxLength(100)]

        public string Title { get; set; }
        [Required, MaxLength(300)]

        public string Description { get; set; }

    }
}
