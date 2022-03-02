﻿namespace TP1.API.Interfaces
{
    public interface IRepository<T>
    {
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
