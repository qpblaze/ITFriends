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
        
        #region Relationships

        // A key can only be assigned to one user
        [ForeignKey("AccountID")]
        public virtual Account Account { get; set; }

        #endregion Relationships
    }
}