using Assignment_Intership.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment_Intership.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Assignment_Intership.Data.Models.Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Data.Models.Task>()
                .HasOne(x => x.Employee)
                .WithMany(x => x.Tasks)
                .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(builder);
        }
    }
}
