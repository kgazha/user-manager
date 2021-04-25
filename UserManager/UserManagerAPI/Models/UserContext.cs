using Microsoft.EntityFrameworkCore;

namespace UserManagerAPI.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
        
        public DbSet<ApplicationUser> Users { get; set; }
    }
}
