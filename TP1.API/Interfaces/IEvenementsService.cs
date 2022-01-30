using System.Collections.Generic;
using TP1.API.Models;

namespace TP1.API.Interfaces
{
    public interface IEvenementsService
    {
        IEnumerable<Evenement> GetList();
        Evenement GetById(int id);
        Evenement Add(Evenement evenement);
        Evenement Update(int id, Evenement evenement);
        void Delete(int id);
    }
}
