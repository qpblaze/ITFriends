using ITFriends.Library.Models;
using System.Threading.Tasks;

namespace ITFriends.Library.Repositories
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Get a user by ID
        /// </summary>
        /// <param name="accountID">User's ID</param>
        /// <returns></returns>
        Task<Account> GetAccountByIDAsync(string accountID);

        /// <summary>
        /// Add a user to the database
        /// </summary>
        /// <param name="account">The user that is goinig to be added</param>
        Task InsertAccountAsync(Account account);

        /// <summary>
        /// Delete a user from the database
        /// </summary>
        /// <param name="accountID">The ID of the user that is going to be deleted</param>
        Task DeleteAccountAsync(string accountID);

        /// <summary>
        /// Update a user from the database
        /// </summary>
        /// <param name="account">The user that is going to be updated</param>
        void UpdateAccount(Account account);

        /// <summary>
        /// Register a user
        /// </summary>
        /// <param name="account">The user that is going to be registred</param>
        Task<ResultStatus> RegisterAccountAsync(Account account);

        /// <summary>
        /// Login a user
        /// </summary>
        /// <param name="account">The account that is trying to sign in</param>
        Task<ResultStatus> LogInAccountAsync(Account account);

        /// <summary>
        /// Save changes to the database
        /// </summary>
        Task SaveAsync();

        /// <summary>
        /// Checks if an email already exists in the database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> VerifyEmailAsync(string email);

        /// <summary>
        /// Confirm user's email
        /// </summary>
        /// <param name="key">Confirmation key</param>
        /// <returns></returns>
        Task<ResultStatus> ConfirmEmailAsync(string key);

        /// <summary>
        /// Send an email to reset the password
        /// </summary>
        /// <returns></returns>
        Task<ResultStatus> ForgotPasswordAsync(string email);
    }
}