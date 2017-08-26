using ITFriends_v2.Models.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ITFriends_v2.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ITFriendsDataContext _dc;
        private readonly IEmailSender _emailSender;

        public AccountRepository(ITFriendsDataContext dc, IEmailSender emailSender)
        {
            _dc = dc;
            _emailSender = emailSender;
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
        /// Register a user
        /// </summary>
        /// <param name="account">The user that is going to be registred</param>
        public async Task RegisterAccount(Account account)
        {
            // Default fields
            account.ID = Guid.NewGuid().ToString();
            account.Role = Roles.User;
            account.Date = DateTime.UtcNow;
            account.EmailVerified = false;
            account.EmailVerificationKey = Guid.NewGuid().ToString();
            account.ProfileImage = "";
            account.CoverImage = "";

            // Hash the password
            account.Password = PasswordHasher(account.Password);

            // Add the account to the database
            InsertAccount(account);

            // Send confirmation email

            // Get the html from wwwroot
            string body = System.IO.File.ReadAllText(@"wwwroot/email/ConfirmEmail.html");

            // Replace the data in the html page
            body.Replace("(name)", account.FirstName);
            body.Replace("(email)", account.Email);
            body.Replace("(link)", _emailSender.Url + "/confirm-email/" + account.EmailVerificationKey);

            // Send the email
            await _emailSender.SendEmailAsync(account.Email, "", body).ConfigureAwait(false);
        }

        /// <summary>
        /// Save changes to the database
        /// </summary>
        public void Save()
        {
            _dc.SaveChanges();
        }

        /// <summary>
        /// Hash a password using PBKDF2 algorithm
        /// </summary>
        /// <param name="password">Original password</param>
        /// <returns>Hashed password</returns>
        private string PasswordHasher(string password)
        {
            // Generate the salt
            byte[] salt = new byte[128 / 8];
            salt = Encoding.ASCII.GetBytes("ITFriends_v2//?");

            // Return hashed password
            return Convert.ToBase64String(KeyDerivation.Pbkdf2
            (
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));
        }
    }
}