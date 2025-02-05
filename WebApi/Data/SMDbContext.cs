using Microsoft.EntityFrameworkCore;

namespace WebApi.Data
{
    public class SMDbContext : DbContext
    {
        public SMDbContext(DbContextOptions<SMDbContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}
