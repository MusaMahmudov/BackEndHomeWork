using System.ComponentModel.DataAnnotations;

namespace HomeWorkPronia.Areas.Admin.ViewModels.CardViewModels
{
    public class CreateCardViewModel
    {
        public IFormFile Image { get; set; }
        [Required, MaxLength(100)]

        public string Title { get; set; }
        [Required, MaxLength(300)]

        public string Description { get; set; }
    }
}
