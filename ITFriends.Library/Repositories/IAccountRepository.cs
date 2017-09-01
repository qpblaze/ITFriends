using ITFriends.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITFriends.Library.Repositories
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Get a list with all users
        /// </summary>
        /// <returns></returns>
        IEnumerable<Account> GetAccounts();

        /// <summary>
        /// Get a user by ID
        /// </summary>
        /// <param name="accountID">User's ID</param>
        /// <returns></returns>
        Account GetAccountByID(int accountID);

        /// <summary>
        /// Add a user to the database
        /// </summary>
        /// <param name="account">The user that is goinig to be added</param>
        Task InsertAccount(Account account);

        /// <summary>
        /// Delete a user from the database
        /// </summary>
        /// <param name="accountID">The ID of the user that is going to be deleted</param>
        void DeleteAccount(int accountID);

        /// <summary>
        /// Update a user from the database
        /// </summary>
        /// <param name="account">The user that is going to be updated</param>
        void UpdateStudent(Account account);

        /// <summary>
        /// Register a user
        /// </summary>
        /// <param name="account">The user that is going to be registred</param>
        Task<ResultStatus> RegisterAccount(Account account);

        /// <summary>
        /// Save changes to the database
        /// </summary>
        void Save();

        /// <summary>
        /// Checks if an email already exists in the database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> VerifyEmail(string email);
    }
}
