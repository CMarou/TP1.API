using System.Collections.Generic;
using TP1.API.DTOs;

namespace TP1.API.Interfaces
{
    public interface IVillesService
    {
        IEnumerable<RequeteVilleDto> GetList();
        IEnumerable<VilleEvenementDto> GetEventsForCity(int id);
        RequeteVilleDto GetById(int id);
        RequeteVilleDto Add(EnvoiVilleDto ville);
        void Update(int id, RequeteVilleDto ville);
        void Delete(int id);
    }
}
