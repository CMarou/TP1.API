using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TP1.API.DTOs;

namespace TP1.API.Interfaces
{
    public interface IEvenementsService
    {
        IEnumerable<RequeteEvenementDto> GetList(int pageIndex, int pageSize);
        IEnumerable<RequeteEvenementDto> GetList(Expression<Func<RequeteEvenementDto, bool>> predicate, int pageIndex, int pageSize);
        IEnumerable<EvenementParticipationDto> GetParticipationsForEvent(int eventId);
        RequeteEvenementDto GetById(int id);
        RequeteEvenementDto Add(EnvoiEvenementDto evenement);
        void Update(int id, RequeteEvenementDto evenement);
        void Delete(int id);

    }
}
