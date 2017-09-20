using System.ComponentModel.DataAnnotations;

namespace ITFriends.Web.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        /// <summary>
        /// The email of the user
        /// </summary>
        [Required(ErrorMessage = "Please enter your email.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
    }
}