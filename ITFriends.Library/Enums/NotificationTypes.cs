namespace ITFriends.Library.Enums
{
    /// <summary>
    /// Used to identify a notification type (friends request, like, share...)
    /// </summary>
    public enum NotificationTypes
    {
        /// <summary>
        /// Used when someone liked user's post
        /// </summary>
        Like = 0,

        /// <summary>
        /// Used when someone commented on user's post
        /// </summary>
        Comment = 1,

        /// <summary>
        /// Used when someone shared user's post
        /// </summary>
        Share = 2,

        /// <summary>
        /// Used when someone send a friend request to the user
        /// </summary>
        FriendRequest = 3,

        /// <summary>
        /// Used when someone accepted user's friend request
        /// </summary>
        FriendRequestAccepted = 4,
    }
}