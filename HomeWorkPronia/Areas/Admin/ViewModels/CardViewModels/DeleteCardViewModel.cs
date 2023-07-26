using System.ComponentModel.DataAnnotations;

namespace HomeWorkPronia.Areas.Admin.ViewModels.CardViewModels
{
    public class DeleteCardViewModel
    {
        public string Image { get; set; }
        [Required, MaxLength(100)]

        public string Title { get; set; }
        [Required, MaxLength(300)]

        public string Description { get; set; }
    }
}
