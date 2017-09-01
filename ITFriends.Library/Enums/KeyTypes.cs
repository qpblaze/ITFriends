namespace ITFriends.Library.Enums
{
    /// <summary>
    /// Used for diffrent keys requested by the user
    /// Those key are send in an email within a link (ex: www.domain.com/email-confirmation/key)
    /// </summary>
    public enum KeyTypes
    {
        /// <summary>
        /// Used to confirm user's email
        /// </summary>
        EmailConfirmation = 0,

        /// <summary>
        /// Used to confirm user's phone number
        /// </summary>
        PhoneConfirmation = 1,

        /// <summary>
        /// Used to reset user's password
        /// </summary>
        PasswordReset = 2,
    }
}