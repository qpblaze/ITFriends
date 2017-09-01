using ITFriends.Library;
using ITFriends.Library.Enums;
using ITFriends.Library.Helpers;
using ITFriends.Library.Models;
using ITFriends.Library.Repositories;
using ITFriends.Web.Models.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITFriends.Web.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ITFriendsDataContext _dc;
        private readonly IEmailSenderRepository _emailSender;

        public AccountRepository(ITFriendsDataContext dc, IEmailSenderRepository emailSender)
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
        public async Task InsertAccount(Account account)
        {
            await _dc.Accounts.AddAsync(account);
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
        public async Task<ResultStatus> RegisterAccount(Account account)
        {

            if(await ValidEmail(account.Email) == false)
            {
                return new ResultStatus(Status.Error, "This email already exists!");
            }

            // Default fields
            account.ID = Guid.NewGuid().ToString();
            account.RoleType = RoleTypes.User;
            account.AddedDate = DateTime.UtcNow;
            account.EmailVerified = false;
            account.ProfileImage = "";
            account.CoverImage = "";
            account.Password = PasswordHash.HashPassword(account.Password);

            // Create email verification key
            Key emailConfirmationKey = new Key()
            {
                ID = Guid.NewGuid().ToString(),
                AddedDate = DateTime.Now,
                AccountID = account.ID,
                KeyType = KeyTypes.EmailConfirmation,
                SecretKey = Guid.NewGuid().ToString()
            };

            account.Keys.Add(emailConfirmationKey);

            // Add the account to the database
            await InsertAccount(account);

            // Get the html from wwwroot
            using (StreamReader sr = File.OpenText(@"wwwroot\email\ConfirmEmail.html"))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(sr.ReadToEnd());

                // Replace the data in the html page
                sb.Replace("(name)", account.FullName);
                sb.Replace("(email)", account.Email);
                sb.Replace("(link)", "/confirm-email/" + emailConfirmationKey.SecretKey);

                // Send the email
                await _emailSender.SendEmailAsync(account.Email, "IT Friends - Email Confirmation", sb.ToString()).ConfigureAwait(false);
            }

            return new ResultStatus(Status.Success);
        }
        
        /// <summary>
        /// Save changes to the database
        /// </summary>
        public void Save()
        {
            _dc.SaveChanges();
        }

        /// <summary>
        /// Checks if an email already exists in the database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private async Task<bool> ValidEmail(string email)
        {
            var account = await _dc.Accounts.FirstOrDefaultAsync(x => x.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));

            if (account == null)
                return true;

            return false;
        }
    }
}