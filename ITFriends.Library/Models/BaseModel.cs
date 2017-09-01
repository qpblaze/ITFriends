using System;

namespace ITFriends.Library.Models
{
    public class BaseModel
    {
        /// <summary>
        /// Identification key
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// The date when the account was created
        /// </summary>
        public DateTime AddedDate { get; set; }
    }
}