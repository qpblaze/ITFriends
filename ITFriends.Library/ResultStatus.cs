namespace ITFriends.Library
{
    public enum Status
    {
        Success = 0,
        Error = 1
    }

    public class ResultStatus
    {
        public ResultStatus(Status status, string message = null)
        {
            Status = status;
            Message = message;
        }

        public Status Status { get; set; }
        public string Message { get; set; }
    }
}