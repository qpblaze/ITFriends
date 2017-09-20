using ITFriends.Library;
using ITFriends.Library.Enums;
using ITFriends.Library.Helpers;
using ITFriends.Library.Models;
using ITFriends.Library.Repositories;
using ITFriends.Library.Services;
using ITFriends.Web.Models.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ITFriends.Web.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ITFriendsDataContext _dc;

        private readonly IKeyRepository _keyRepository;
        private readonly IEmailSenderRepository _emailSender;
        
        private readonly string _url;

        public AccountRepository(ITFriendsDataContext dc, IEmailSenderRepository emailSender, string url)
        {
            _dc = dc;
            _emailSender = emailSender;
            _keyRepository = new KeyRepository(_dc);
            _url = url;
        }

        public async Task<Account> GetAccountByIDAsync(string accountID)
        {
            return await _dc.Accounts.FindAsync(accountID);
        }

        /// <summary>
        /// Add a user to the database and add the default fields
        /// </summary>
        /// <param name="account">The user that is goinig to be added</param>
        public async Task InsertAccountAsync(Account account)
        {
            account.ID = Guid.NewGuid().ToString();
            account.AddedDate = DateTime.UtcNow;

            await _dc.Accounts.AddAsync(account);
        }

        public async Task DeleteAccountAsync(string accountID)
        {
            var account = await GetAccountByIDAsync(accountID);

            if(account != null)
            {
                _dc.Accounts.Remove(account);
            }
            
        }
        
        public void UpdateAccount(Account account)
        {
            _dc.Entry(account).State = EntityState.Modified;
        }
        
        public async Task<ResultStatus> RegisterAccountAsync(Account account)
        {
            if (await VerifyEmailAsync(account.Email) == false)
            {
                return new ResultStatus(Status.Error, "Email", "This email already exists!");
            }

            // Default fields
            account.RoleType = RoleTypes.User;
            account.EmailVerified = false;
            account.ProfileImage = "";
            account.CoverImage = "";
            account.Password = PasswordHash.HashPassword(account.Password);

            // Create and add email verification key
            string emailConfirmationKey = await _keyRepository.InserKeyAsync(account.ID);
            
            // Add the account to the database
            await InsertAccountAsync(account);

            // Get the html from wwwroot
            using (StreamReader sr = File.OpenText(@"wwwroot\email\ConfirmEmail.html"))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(sr.ReadToEnd());

                // Replace the data in the html page
                sb.Replace("(name)", account.FullName);
                sb.Replace("(email)", account.Email);
                sb.Replace("(link)", $"{ _url }/confirm-email/{ emailConfirmationKey }");

                // Send the email
                await _emailSender.SendEmailAsync(account.Email, "Email Confirmation", sb.ToString()).ConfigureAwait(false);
            }

            await SaveAsync();

            return new ResultStatus(Status.Success);
        }
        
        public async Task SaveAsync()
        {
            await _dc.SaveChangesAsync();
        }

        /// <summary>
        /// Checks if an email already exists in the database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> VerifyEmailAsync(string email)
        {
            var account = await _dc.Accounts.FirstOrDefaultAsync(x => x.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));

            if (account == null)
                return true;

            return false;
        }

        /// <summary>
        /// Confirm user's email
        /// </summary>
        /// <param name="key">Confirmation key</param>
        /// <returns></returns>
        public async Task<ResultStatus> ConfirmEmailAsync(string secretKey)
        {
            Key key = await _dc.Keys.FirstOrDefaultAsync(x => x.SecretKey == secretKey);

            if (key == null)
                return new ResultStatus(Status.Error, "", "Invalid key.");

            // Validate the email
            Account account = await GetAccountByIDAsync(key.AccountID);
            account.EmailVerified = true;
            UpdateAccount(account);

            // Remove the key
            _keyRepository.DeleteKey(key);

            // Save changes
            await SaveAsync();

            return new ResultStatus(Status.Success);
        }

        /// <summary>
        /// Login a user
        /// </summary>
        /// <param name="account">The account that is trying to sign in</param>
        public async Task<ResultStatus> LogInAccountAsync(Account signInModel)
        {
            Account account = await _dc.Accounts.FirstOrDefaultAsync(x => x.Email.Equals(signInModel.Email, StringComparison.InvariantCultureIgnoreCase));

            if (account == null)
                return new ResultStatus(Status.Error, "Email", "Invalid email or password.");

            if (PasswordHash.ValidatePassword(signInModel.Password, account.Password) == false)
                return new ResultStatus(Status.Error, "Email", "Invalid email or password.");

            if (account.EmailVerified == false)
                return new ResultStatus(Status.Error, "Email", "The email was not confirmed.");

            return new ResultStatus(Status.Success, returnedObject: account);
        }

        /// <summary>
        /// Send an email to reset the password
        /// </summary>
        /// <returns></returns>
        public async Task<ResultStatus> ForgotPasswordAsync(string email)
        {
            var account = await _dc.Accounts.FirstOrDefaultAsync(x => x.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));

            if (account == null)
                return new ResultStatus(Status.Error, "Email", "This email is not linked to an account.");

            // Create email verification key
            Key forgotPasswordKey = new Key()
            {
                ID = Guid.NewGuid().ToString(),
                AddedDate = DateTime.Now,
                AccountID = account.ID,
                SecretKey = Guid.NewGuid().ToString()
            };

            // Add the key to the database
            await _dc.Keys.AddAsync(forgotPasswordKey);
            await SaveAsync();

            // Get the html from wwwroot
            using (StreamReader sr = File.OpenText(@"wwwroot\email\RecoverPassword.html"))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(sr.ReadToEnd());

                // Replace the data in the html page
                sb.Replace("(name)", account.FullName);
                sb.Replace("(email)", account.Email);
                sb.Replace("(link)", $"{ _url }/reset-password/{ forgotPasswordKey.SecretKey }");

                // Send the email
                await _emailSender.SendEmailAsync(account.Email, "Password Reset", sb.ToString()).ConfigureAwait(false);
            }

            return new ResultStatus(Status.Success);
        }
    }
}