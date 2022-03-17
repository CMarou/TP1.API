using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TP1.API.DTOs;
using TP1.API.Interfaces;

namespace TP1.API.Data.Repository
{
    public interface IEvenementRepository : IRepository<RequeteEvenementDto, EnvoiEvenementDto>
    {
        IEnumerable<RequeteEvenementDto> GetList(int pageIndex, int pageSize);
        IEnumerable<RequeteEvenementDto> GetList(Expression<Func<RequeteEvenementDto, bool>> predicate, int pageIndex, int pageSize);
        IEnumerable<RequeteEvenementDto> GetList(Expression<Func<RequeteEvenementDto, bool>> predicate);
        IEnumerable<EvenementParticipationDto> GetParticipationsForEvent(int eventId);
        double GetTotalSalesAmountFromEvent(int eventId);
    }
}
