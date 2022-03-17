using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TP1.API.DTOs;

namespace TP1.API.Interfaces
{
    public interface IParticipationsService
    {
        IEnumerable<RequeteParticipationDto> GetList();
        IEnumerable<RequeteParticipationDto> GetList(Expression<Func<RequeteParticipationDto, bool>> predicat);
        RequeteParticipationDto GetById(int id);
        RequeteParticipationDto Add(EnvoiParticipationDto participation);
        void Activate(int id);
        void Delete(int id);
        bool Exists(int id);
    }
}
