using System.ComponentModel.DataAnnotations;

namespace HomeWorkPronia.ViewModels.AccountViewModels
{
    public class CreateAccountViewModel
    {
        [Required,MaxLength(256)]
        public string Fullname { get; set; }
        [Required,MaxLength (256)]
        public string userName {  get; set; }
        [Required,DataType(DataType.EmailAddress),MaxLength(256)]
        public string Email { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        [Required,DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword { get; set;}
    }
}
