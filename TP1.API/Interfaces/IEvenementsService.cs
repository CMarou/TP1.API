using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TP1.API.Models;

namespace TP1.API.Interfaces
{
    public interface IEvenementsService
    {
        IEnumerable<Evenement> GetList();
        IEnumerable<Evenement> GetList(Func<Evenement, bool> predicat);
        Evenement GetById(int id);
        Evenement Add(Evenement evenement);
        Evenement Update(int id, Evenement evenement);
        void Delete(int id);

    }
}
