using HomeWorkPronia.Utils.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeWorkPronia.Models.Identity
{
    public class AppRole : IdentityRole
    {
        [NotMapped]
        public virtual ICollection<AppUser>? Users { get; set; }
        public AppRole()
        {
            Users = new List<AppUser>();
        }

    }
}
