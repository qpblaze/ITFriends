using ITFriends_v2.Models;
using ITFriends_v2.Models.AccountViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ITFriends_v2.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ITFriendsDataContext _dc;

        public AccountRepository(ITFriendsDataContext dc)
        {
            _dc = dc;
        }

        /// <summary>
        /// Get a list with all users
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Account> GetAccounts()
        {
            return _dc.Accounts.ToList();
        }

        /// <summary>
        /// Get a user by ID
        /// </summary>
        /// <param name="accountID">User's ID</param>
        /// <returns></returns>
        public Account GetAccountByID(int accountID)
        {
            return _dc.Accounts.Find(accountID);
        }

        /// <summary>
        /// Add a user to the database
        /// </summary>
        /// <param name="account">The user that is goinig to be added</param>
        public void InsertAccount(Account account)
        {
            _dc.Accounts.Add(account);
        }

        /// <summary>
        /// Delete a user from the database
        /// </summary>
        /// <param name="accountID">The ID of the user that is going to be deleted</param>
        public void DeleteAccount(int accountID)
        {
            Account account = _dc.Accounts.Find(accountID);
            _dc.Accounts.Remove(account);
        }

        /// <summary>
        /// Update a user from the database
        /// </summary>
        /// <param name="account">The user that is going to be updated</param>
        public void UpdateStudent(Account account)
        {
            _dc.Entry(account).State = EntityState.Modified;
        }

        /// <summary>
        /// Save changes to the database
        /// </summary>
        public void Save()
        {
            _dc.SaveChanges();
        }
    }
}