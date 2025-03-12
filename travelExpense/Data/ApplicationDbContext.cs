using Microsoft.EntityFrameworkCore;
using travelExpense.Models;

namespace travelExpense.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Travel> Travels { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Travel>()
                .HasOne(t => t.Category)
                .WithMany()
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Travel>()
                .HasMany(t => t.Clients)
                .WithMany(c => c.Travels)
                .UsingEntity<Dictionary<string, object>>(
                    "TravelClient",
                    r => r.HasOne<Client>().WithMany().HasForeignKey("ClientId"),
                    r => r.HasOne<Travel>().WithMany().HasForeignKey("TravelId")
                );

            modelBuilder.Entity<Expense>()
                .HasOne(e => e.Travel)
                .WithMany(t => t.Expenses)
                .HasForeignKey(e => e.TravelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Role>()
                .HasIndex(r => r.RoleName)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Client>()
                .HasIndex(c => c.Email)
                .IsUnique();
        }
    }
}
