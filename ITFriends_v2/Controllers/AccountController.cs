using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ITFriends_v2.Repositories;
using AutoMapper;
using ITFriends_v2.Models.Services;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;

namespace ITFriends_v2.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public AccountController(IMapper mapper, IEmailSender emailSender, ITFriendsDataContext dc)
        {
            _mapper = mapper;

            // Set the domain url
            // TODO
            emailSender.Url = "it-friends.azurewebsites.net";
            _accountRepository = new AccountRepository(dc, emailSender);
        }

        #region SignIn

        [Route("sign-in")]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [Route("sign-in")]
        public IActionResult SignIn(SignInViewModel model)
        {
            return View();
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

            await _accountRepository.RegisterAccount(account).ConfigureAwait(false);
            _accountRepository.Save();

            return RedirectToAction("SignIn");
        }

        #endregion SignUp

        #region ResetPassword

        [Route("reset-password")]
        public IActionResult ResetPassword()
        {
            return View();
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
        public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            return View();
        }

        #endregion ForgotPassword
    }
}