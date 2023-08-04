using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EShopper.Models
{
    public class AppUser:IdentityUser
    {
        [StringLength(maximumLength: 100, ErrorMessage = "FullName is too long!")]
        public string FullName { get; set; }
    }
}
