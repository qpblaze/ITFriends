using ITFriends.Library.Models;
using System.Threading.Tasks;

namespace ITFriends.Library.Repositories
{
    public interface IKeyRepository
    {
        /// <summary>
        /// Get a key from the database
        /// </summary>
        /// <param name="secretKey">Yhe key that is provided via email</param>
        /// <returns></returns>
        Task<Key> GetKeyByIDAsync(string secretKey);

        /// <summary>
        /// Add a key to the database
        /// </summary>
        /// <param name="accountID">The key that is goinig to be added</param>
        Task<string> InserKeyAsync(string accountID);

        /// <summary>
        /// Delete a key from the database
        /// </summary>
        /// <param name="secretKey">The secretKey of the key that is going to be deleted</param>
        Task DeleteKeyAsync(string secretKey);
        void DeleteKey(Key key);

        /// <summary>
        /// Save changes to the database
        /// </summary>
        Task SaveAsync();
    }
}