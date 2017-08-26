using ITFriends_v2.Models.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ITFriends_v2.Pages
{
    public class ForgotPasswordModel : PageModel
    {
        [BindProperty]
        public ForgotPasswordViewModel Account { get; set; }

        public void OnGet()
        {

        }
    }
}