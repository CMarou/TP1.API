using System.Collections.Generic;
using TP1.API.Data.Models;

namespace TP1.API.Interfaces
{
    public interface IVillesService
    {
        IEnumerable<Ville> GetList();
        Ville GetById(int id);
        Ville Add(Ville ville);
        Ville Update(int id, Ville ville);
        void Delete(int id);
    }
}
