using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ITFriends.Web.Models.AccountViewModels
{
    public class SignUpViewModel
    {
        /// <summary>
        /// The email of the user
        /// </summary>
        [Required(ErrorMessage = "Please enter your email.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Remote(action: "VerifyEmail", controller: "Account")]
        public string Email { get; set; }

        /// <summary>
        /// The password of the user
        /// </summary>
        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Confirm password field
        /// </summary>
        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Your password and confirmation password do not match.")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// First name of the user
        /// </summary>
        [Required(ErrorMessage = "Please enter your first name.")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the user
        /// </summary>
        [Required(ErrorMessage = "Please enter your last name.")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
    }
}