using Microsoft.EntityFrameworkCore;
using PocketApi.Models;

namespace PocketApi.Data
{
    public class PocketBudgetContext(DbContextOptions<PocketBudgetContext> options) : DbContext(options)
    {
        public DbSet<PocketUser> Users { get; set; }

        public DbSet<PocketUserSettings> UserSettings { get; set; }

        public DbSet<PocketExpense> Expenses { get; set; }
        
        // Family

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PocketUser>()
                .HasMany<PocketExpense>(x => x.Expenses).WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<PocketUser>().HasOne(x => x.Settings).WithOne(x => x.User)
                .HasForeignKey<PocketUserSettings>(x => x.UserId);
        }
    }
}