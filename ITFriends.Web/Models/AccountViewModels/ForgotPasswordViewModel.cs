﻿using System.ComponentModel.DataAnnotations;

namespace ITFriends.Web.Models.AccountViewModels
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