using Microsoft.EntityFrameworkCore;

namespace ITFriends_v2
{
    public class ITFriendsDataContext : DbContext
    {
        public ITFriendsDataContext(DbContextOptions<ITFriendsDataContext> options)
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