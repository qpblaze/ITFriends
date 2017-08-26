﻿using System.ComponentModel.DataAnnotations;

namespace ITFriends_v2.Models.AccountViewModels
{
    public class SignInViewModel
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
    }
}