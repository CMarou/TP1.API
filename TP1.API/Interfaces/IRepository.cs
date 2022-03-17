namespace TP1.API.Interfaces
{
    public interface IRepository<T, TNew>
    {
        T GetById(int id);
        T Add(TNew entity);
        void Update(T entity);
        void Delete(int id);
    }
}
