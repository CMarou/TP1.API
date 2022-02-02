using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TP1.API.Models;

namespace TP1.API.Interfaces
{
    public interface IParticipationsService
    {
        IEnumerable<Participation> GetList();
        IEnumerable<Participation> GetList(Func<Participation, bool> predicat);
        Participation GetById(int id);
        Participation Add(Participation participation);
        void Delete(int id);
    }
}
