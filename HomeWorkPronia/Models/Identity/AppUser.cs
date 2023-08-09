using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeWorkPronia.Models.Identity
{
    public class AppUser : IdentityUser
    {
        [Required,MaxLength(256)]
        public string Fullname { get; set; }
        public bool IsActive {  get; set; }
        [NotMapped]
        public virtual ICollection<AppRole>? Roles { get; set; }
        public AppUser() 
        {
            Roles = new List<AppRole>();
        }

    }
}
