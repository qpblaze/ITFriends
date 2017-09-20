using AutoMapper;
using ITFriends.Library;
using ITFriends.Library.Helpers;
using ITFriends.Library.Models;
using ITFriends.Library.Repositories;
using ITFriends.Web.Models.AccountViewModels;
using ITFriends.Web.Models.Services;
using ITFriends.Web.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ITFriends.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITFriendsDataContext _dc;

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public AccountController(IMapper mapper, IEmailSenderRepository emailSender, ITFriendsDataContext dc, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            string url = $"{ _httpContextAccessor.HttpContext.Request.Scheme }://{ _httpContextAccessor.HttpContext.Request.Host }";

            _mapper = mapper;
            _dc = dc;
            _accountRepository = new AccountRepository(dc, emailSender, url);  
        }

        #endregion Constructor

        #region SignIn

        [Route("sign-in")]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [Route("sign-in")]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            Account account = _mapper.Map<SignInViewModel, Account>(model);
            var result = await _accountRepository.LogInAccountAsync(account);

            if(result.Status != Status.Success)
            {
                ModelState.AddModelError(result.ErrorFor, result.Message);
                return View(model);
            }

            account = (Account)result.ReturnedObject;

            // Add claims
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, account.FullName, ClaimValueTypes.String),
                new Claim("ProfileImage", account.ProfileImage, ClaimValueTypes.String)
            };

            var identity = new ClaimsIdentity(claims);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        #endregion SignIn

        #region SignUp

        [Route("sign-up")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [Route("sign-up")]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            Account account = _mapper.Map<SignUpViewModel, Account>(model);

            var result = await _accountRepository.RegisterAccountAsync(account).ConfigureAwait(false);

            if (result.Status != Status.Success)
            {
                ModelState.AddModelError(result.ErrorFor, result.Message);
                return View(model);
            }
            await _accountRepository.SaveAsync();

            return RedirectToAction("SignIn");
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> VerifyEmail(string email)
        {
            if (await _accountRepository.VerifyEmailAsync(email) == false)
            {
                return Json(data: "This email is already in use.");
            }

            return Json(data: true);
        }

        #endregion SignUp

        #region ResetPassword

        [Route("reset-password/{secretKey}")]
        public async Task<IActionResult> ResetPassword(string secretKey)
        {
           // var account = await _key.ValidKey(secretKey);
            //if(account != null)
            //{
            //    return View(_mapper.Map<Account, ResetPasswordViewModel>(account));
            //}

            return View("SignIn");
        }

        [HttpPost]
        [Route("reset-password")]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            return View();
        }

        #endregion ResetPassword

        #region ForgotPassword

        [Route("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            var result = await _accountRepository.ForgotPasswordAsync(model.Email);

            if(result.Status != Status.Success)
            {
                ModelState.AddModelError(result.ErrorFor, result.Message);
                return View(model);
            }

            return RedirectToAction("SignIn");
        }

        #endregion ForgotPassword

        #region Confirm Email
        
        [Route("confirm-email/{secretKey}")]
        public async Task<IActionResult> ConfirmEmail(string secretKey)
        {
            var result = await _accountRepository.ConfirmEmailAsync(secretKey);

            if (result.Status != Status.Success)
                throw new Exception("Error - Confirm Email");

            return View();
        }

        #endregion Confirm Email
    }
}