using System.Collections;
using System.Net;

namespace fraturas_csharp.Data.DTOs
{
    public interface IResponseMessages
    {
        string Message { get; set; }
        object Data { get; set; }
        public void Add(IResponseMessages responseMessage);
        public void Remove(IResponseMessages responseMessage);
        public void Clear();
    }
    public class ResponseMessages : IResponseMessages
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }

        public ResponseMessages(string message, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Message = message;
            Data = null;
            StatusCode = statusCode;
        }
        public ResponseMessages(string message, object data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Message = message;
            Data = data;
            StatusCode = statusCode;
        }
        public ResponseMessages(object data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Message = string.Empty;
            Data = data;
            StatusCode = statusCode;
        }
        public void Add(IResponseMessages responseMessage)
        {
            Add(responseMessage);
        }
        public void Remove(IResponseMessages responseMessage)
        {
            Remove(responseMessage);
        }
        public void Clear()
        {
            Clear();
        }
    }
}