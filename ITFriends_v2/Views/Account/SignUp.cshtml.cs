using ITFriends_v2.Models.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ITFriends_v2.Pages
{
    public class SignUpModel : PageModel
    {
        [BindProperty]
        public SignUpViewModel Account { get; set; }

        public void OnGet()
        {

        }
    }
}