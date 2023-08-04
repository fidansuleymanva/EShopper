using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EShopper.Models
{
    public class DataContext:IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Slider> Slider { get; set; }

    }
}
