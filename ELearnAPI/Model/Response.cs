namespace ELearnAPI.Model
{
    public class Response
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }
    }

    public enum ResponseType
    {
        Success,
        NotFound,
        Failure
    }
}
