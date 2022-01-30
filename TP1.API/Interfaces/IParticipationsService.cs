using System.Collections.Generic;
using TP1.API.Models;

namespace TP1.API.Interfaces
{
    public interface IParticipationsService
    {
        IEnumerable<Participation> GetList();
        Participation GetById(int id);
        Participation Add(Participation participation);
        void Delete(int id);
    }
}
