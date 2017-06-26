using belt.Models;
using Microsoft.EntityFrameworkCore;
 
namespace belt.Models
{
    
    public class YourContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public YourContext(DbContextOptions<YourContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Invitation> Invitations { get; set; }

        //dont forget to add these!

    }
}