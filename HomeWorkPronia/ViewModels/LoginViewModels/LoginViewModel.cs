using System.ComponentModel.DataAnnotations;

namespace HomeWorkPronia.ViewModels.LoginViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UsernameOrEmail { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
