using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TP1.API.Interfaces
{
    public interface IListRepository<T>
    {
        IEnumerable<T> GetList();
        IEnumerable<T> GetList(Expression<Func<T, bool>> predicate);
    }
}
