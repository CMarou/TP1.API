using TP1.API.Exceptions;
using TP1.API.Interfaces;

namespace TP1.API.Services
{
    public class HttpExceptionThrower : IHttpExceptionThrower
    {
        public void ThrowHttpException(int statusCode, params string[] erreurs)
        {
            throw new HttpException
            {
                StatusCode = statusCode,
                Errors = erreurs
            };
        }
    }
}
