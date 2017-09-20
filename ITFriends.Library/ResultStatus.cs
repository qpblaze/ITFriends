namespace ITFriends.Library
{
    public enum Status
    {
        Success = 0,
        Error = 1
    }

    public class ResultStatus
    {
        public ResultStatus(Status status,string errorFor = null, string message = null, dynamic returnedObject = null)
        {
            Status = status;
            Message = message;
            ErrorFor = errorFor;
            ReturnedObject = returnedObject;
        }

        public Status Status { get; set; }
        public string Message { get; set; }
        public string ErrorFor { get; set; }
        public dynamic ReturnedObject { get; set; }
    }
}