using CIRMS_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace CIRMS_Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}