using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TP1.API.Data.Models;
using TP1.API.Interfaces;

namespace TP1.API.Data.Repository
{
    public interface IEvenementRepository : IRepository<Evenement>
    {
        IEnumerable<Evenement> GetList(int pageIndex, int pageSize);
        IEnumerable<Evenement> GetList(Expression<Func<Evenement, bool>> predicate, int pageIndex, int pageSize);
        IEnumerable<Participation> GetParticipationsForEvent(int eventId);
        double GetTotalSalesAmountFromEvent(int eventId);
    }
}
