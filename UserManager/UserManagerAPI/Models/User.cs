using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagerAPI.Models
{
    public class User : IdentityUser<int>
    {
        [Column(TypeName = "nvarchar(100)")]
        public override string UserName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public override string Email { get; set; }

        [NotMapped]
        public string Password { get; set; }
    }
}
