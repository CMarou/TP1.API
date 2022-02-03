using TP1.API.Exceptions;

namespace TP1.API.Interfaces
{
    public interface IHttpExceptionThrower
    {
        public void ThrowHttpException(int statusCode, params string[] erreurs);
    }
}