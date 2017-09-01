using ITFriends.Library.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITFriends.Library.Models
{
    public class Key : BaseModel
    {
        /// <summary>
        /// The ID of the user's key
        /// </summary>
        public string AccountID { get; set; }

        /// <summary>
        /// The key that is stored (usually a Guid)
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// User's key id which is store in database
        /// </summary>
        public virtual int KeyTypeID
        {
            get
            {
                return (int)this.KeyType;
            }
            set
            {
                KeyType = (KeyTypes)value;
            }
        }

        /// <summary>
        /// User key that can be accessed anywhere
        /// </summary>
        [NotMapped]
        public KeyTypes KeyType { get; set; }

        #region Relationships

        // A key can only be assigned to one user
        [ForeignKey("AccountID")]
        public virtual Account Account { get; set; }

        #endregion Relationships
    }
}