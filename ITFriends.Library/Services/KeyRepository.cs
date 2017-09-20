using System.Threading.Tasks;
using ITFriends.Library.Models;
using ITFriends.Library.Repositories;
using System;

namespace ITFriends.Library.Services
{
    public class KeyRepository : IKeyRepository
    {
        private readonly ITFriendsDataContext _dc;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dc"></param>
        public KeyRepository(ITFriendsDataContext dc)
        {
            _dc = dc;
        }

        public async Task DeleteKeyAsync(string secretKey)
        {
            var key = await GetKeyByIDAsync(secretKey);

            if (key != null)
            {
                _dc.Keys.Remove(key);
            }
        }

        public void DeleteKey(Key key)
        {
            if (key != null)
            {
                _dc.Keys.Remove(key);
            }
        }

        public async Task<Key> GetKeyByIDAsync(string secretKey)
        {
            return await _dc.Keys.FindAsync(secretKey);
        }

        /// <summary>
        /// Generate and bind a key to an account
        /// </summary>
        /// <param name="accountID">ID of the account that this key is going to be bind to</param>
        /// <returns>The generated secret key</returns>
        public async Task<string> InserKeyAsync(string accountID)
        {
            // Create a new key and set the properties
            Key key = new Key()
            {
                ID = Guid.NewGuid().ToString(),
                AddedDate = DateTime.UtcNow,
                SecretKey = Guid.NewGuid().ToString(),
                AccountID = accountID
            };
            
            // Add the key to the database
            await _dc.Keys.AddAsync(key);

            return key.SecretKey;
        }

        public async Task SaveAsync()
        {
            await _dc.SaveChangesAsync();
        }
    }
}