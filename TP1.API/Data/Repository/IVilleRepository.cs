using System.Collections.Generic;
using TP1.API.Data.Models;
using TP1.API.DTOs;
using TP1.API.Interfaces;

namespace TP1.API.Data.Repository
{
    public interface IVilleRepository : IRepository<RequeteVilleDto, EnvoiVilleDto>, IListRepository<RequeteVilleDto>
    {
        IEnumerable<VilleEvenementDto> GetEventsForCity(int cityId);
        IEnumerable<RequeteVilleDto> GetCitiesByEventCountDescending();
    }
}
