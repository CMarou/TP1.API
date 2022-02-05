using System;

namespace TP1.API.Exceptions
{
    public class HttpException : Exception
    {
        public int StatusCode { get; set; }
        public object Value { get; set; }

        public HttpException()
        {
        }

        public HttpException(int statusCode, params string[] erreurs)
        {
            StatusCode = statusCode;
            Value = new { Errors = erreurs };
        }
    }
}
