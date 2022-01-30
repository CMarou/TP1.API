using System;
using System.Collections.Generic;

namespace TP1.API.Exceptions
{
    public class HttpException : Exception
    {
        public int StatusCode { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
