using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITFriends_v2
{
    public class Account
    {
        /// <summary>
        /// Identification key
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// User's role id which is store in database
        /// </summary>
        public virtual int RoleID
        {
            get
            {
                return (int)this.Role;
            }
            set
            {
                Role = (Roles)value;
            }
        }
         
        /// <summary>
        /// User role that can be accessed anywhere
        /// </summary>
        [NotMapped]
        public Roles Role { get; set; }

        /// <summary>
        /// The date when the account was created
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The link to the profile image
        /// </summary>
        public string ProfileImage { get; set; }

        /// <summary>
        /// The link to the cover image
        /// </summary>
        public string CoverImage { get; set; }

        /// <summary>
        /// User's email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// True if the email is verified, false if not
        /// </summary>
        public bool EmailVerified { get; set; }

        /// <summary>
        /// This key is send via Email when the user registers
        /// </summary>
        public string EmailVerificationKey { get; set; }

        /// <summary>
        /// User's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User's full name automaticaly created from <see cref="FirstName"/> and <see cref="LastName"/> when needed
        /// </summary>
        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        /// <summary>
        /// User's encrypted password
        /// </summary>
        public string Password { get; set; }
    }
}