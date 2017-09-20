using ITFriends.Library.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITFriends.Library.Models
{
    public class Account : BaseModel
    {
        public Account()
        {
            Keys = new List<Key>();
        }

        /// <summary>
        /// User's role id which is store in database
        /// </summary>
        public virtual int RoleTypeID
        {
            get
            {
                return (int)this.RoleType;
            }
            set
            {
                RoleType = (RoleTypes)value;
            }
        }

        /// <summary>
        /// User role that can be accessed anywhere
        /// </summary>
        [NotMapped]
        public RoleTypes RoleType { get; set; }

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

        #region Relationships

        // A user can have more keys
        public ICollection<Key> Keys { get; set; }

        #endregion Relationships

    }
}