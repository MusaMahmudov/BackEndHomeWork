﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeWorkPronia.ViewModels.LoginViewModels;

public class ResetPasswordViewModel
{
    [Required,DataType(DataType.Password)]
    public string NewPassword { get; set; }
    [Required, DataType(DataType.Password),Compare(nameof(NewPassword))]
    public string ConfirmPassword { get; set;}


}
