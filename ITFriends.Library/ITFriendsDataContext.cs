using ITFriends.Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace ITFriends.Library
{
    public class ITFriendsDataContext : DbContext
    {
        public ITFriendsDataContext(DbContextOptions<ITFriendsDataContext> options)
            : base(options)
        {
            Database.EnsureCreated();

            //RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator)Database.GetService<IDatabaseCreator>();
            //databaseCreator.CreateTables();
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Key> Keys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable(nameof(Accounts));
            modelBuilder.Entity<Key>().ToTable(nameof(Keys));
        }
    }
}