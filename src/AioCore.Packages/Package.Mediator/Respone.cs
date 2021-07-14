using System.Net;

namespace Package.Mediator
{
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HttpStatusCode Status { get; set; }
        public int StatusCode => (int)Status;
    }

    public class Response<T> : Response
    {
        public T Data { get; set; }

        public static implicit operator Response<T>(T value)
        {
            return new()
            {
                Success = true,
                Status = HttpStatusCode.OK,
                Data = value
            };
        }
    }
}