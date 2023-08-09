﻿using Microsoft.AspNetCore.Identity;

namespace HomeWorkPronia.Areas.Admin.ViewModels.UserViewModels
{
    public class UserViewModel 
    {
        public string Id { get; set; }
        public string FullName { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
