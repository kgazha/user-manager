using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagerAPI.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public override string UserName { get; set; }
        public override string Email { get; set; }
    }
}
