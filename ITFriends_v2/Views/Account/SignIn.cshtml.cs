using ITFriends_v2.Models.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ITFriends_v2.Pages
{
    public class SignInModel : PageModel
    {
        [BindProperty]
        public SignInViewModel Account { get; set; }

        public void OnGet()
        {

        }

        public void OnPost()
        {

        }

    }
}