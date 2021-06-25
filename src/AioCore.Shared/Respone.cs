namespace AioCore.Shared
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public string ContentType { get; set; } = "application/json; charset=utf-8";
        public T Data { get; set; }
    }
}
