using System.Collections.Generic;
using TP1.API.Data.Models;
using TP1.API.Interfaces;

namespace TP1.API.Data.Repository
{
    public interface IVilleRepository : IRepository<Ville>, IListRepository<Ville>
    {
        IEnumerable<Evenement> GetEventsForCity(int cityId);
        IEnumerable<Ville> GetCitiesByEventCountDescending();
    }
}
