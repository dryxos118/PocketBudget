using Microsoft.EntityFrameworkCore;
using PocketApi.Models;

namespace PocketApi.Data
{
    public class PocketBudgetContext(DbContextOptions<PocketBudgetContext> options) : DbContext(options)
    {
        public DbSet<PocketUser> Users { get; set; }

        public DbSet<PocketRole> Roles { get; set; }

        public DbSet<PocketExpense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PocketUser>()
                .HasMany<PocketExpense>(x => x.Expenses).WithOne(x => x.User).HasForeignKey(x => x.UserId);

            modelBuilder.Entity<PocketUser>()
                .HasMany<PocketRole>(x => x.Roles).WithMany(x => x.Users).UsingEntity(j => j.ToTable("UserRoles"));

            modelBuilder.Entity<PocketRole>().HasData(
                new PocketRole { RoleId = 1, RoleName = "PocketUser" },
                new PocketRole { RoleId = 2, RoleName = "PocketAdmin" }
            );
        }
    }
}
