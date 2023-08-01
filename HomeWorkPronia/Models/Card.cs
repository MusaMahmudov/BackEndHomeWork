using HomeWorkPronia.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace HomeWorkPronia.Models
{
    public class Card : BaseEntity
    {
        public string Image { get; set; }
        [Required, MaxLength(100)]

        public string Title { get; set; }
        [Required, MaxLength(300)]

        public string Description { get; set; }
        public bool IsDeleted { get; set; }

    }
}
