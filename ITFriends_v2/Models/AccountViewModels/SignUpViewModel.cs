using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ITFriends_v2.Models.AccountViewModels
{
    public class SignUpViewModel
    {
        /// <summary>
        /// The email of the user
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// The password of the user
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Confirm password field
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// First name of the user
        /// </summary>
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the user
        /// </summary>
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
    }
}