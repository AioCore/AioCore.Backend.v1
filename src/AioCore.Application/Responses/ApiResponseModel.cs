using System.Net;

namespace AioCore.Application.Responses
{
    public class ApiResponseModel<T>
    {
        public HttpStatusCode ResponseType { get; set; }

        public T Data { get; set; }

        public string Message { get; set; }
    }
}