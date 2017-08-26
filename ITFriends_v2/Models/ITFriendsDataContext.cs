using Microsoft.EntityFrameworkCore;
using ITFriends_v2.Models.AccountViewModels;

namespace ITFriends_v2.Models
{
    public class ITFriendsDataContext : DbContext
    {
        public ITFriendsDataContext(DbContextOptions<DbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable(nameof(Accounts));
        }
    }
}