using AutoMapper;
using ITFriends.Library;
using ITFriends.Library.Models;
using ITFriends.Library.Repositories;
using ITFriends.Web.Models.AccountViewModels;
using ITFriends.Web.Models.Services;
using ITFriends.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ITFriends.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public AccountController(IMapper mapper, IEmailSenderRepository emailSender, ITFriendsDataContext dc)
        {
            _mapper = mapper;

            // Set the domain url
            // TODO
            _accountRepository = new AccountRepository(dc, emailSender);
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
            
            var result = await _accountRepository.RegisterAccount(account).ConfigureAwait(false);

            if (result.Status != Status.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }
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