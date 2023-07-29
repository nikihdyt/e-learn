namespace ELearnAPI.Model
{
    public class Response
    {
        private object value;

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
