using System.ComponentModel.DataAnnotations;

namespace HomeWorkPronia.Areas.Admin.ViewModels.CardViewModels
{
	public class UpdateCardViewModel
	{ 
		public int Id { get; set; }
		public IFormFile? Image { get; set; }
		[Required, MaxLength(100)]

		public string Title { get; set; }
		[Required, MaxLength(300)]

		public string Description { get; set; }
	}
}
