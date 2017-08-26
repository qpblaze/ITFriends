using System.ComponentModel.DataAnnotations;

namespace ITFriends_v2.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        /// <summary>
        /// The email of the user
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}