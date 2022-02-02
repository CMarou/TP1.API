namespace TP1.API.Interfaces
{
    public interface IHttpExceptionThrower
    {
        public void ThrowHttpException(int statusCode, params string[] erreurs);
    }
}