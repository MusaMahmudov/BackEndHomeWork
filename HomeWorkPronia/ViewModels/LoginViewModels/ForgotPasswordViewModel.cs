using System.ComponentModel.DataAnnotations;

namespace HomeWorkPronia.ViewModels.LoginViewModels;

public class ForgotPasswordViewModel
{
    [Required,DataType(DataType.EmailAddress)]
    public string Email { get; set; }
   
}
