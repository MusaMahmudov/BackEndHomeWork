using HomeWorkPronia.Utils.Enums;
using Microsoft.AspNetCore.Identity;

namespace HomeWorkPronia.Areas.Admin.ViewModels.UserViewModels
{
    public class DetailUserViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }


    }
}
