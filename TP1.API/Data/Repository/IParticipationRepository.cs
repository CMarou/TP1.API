using TP1.API.Data.Models;
using TP1.API.DTOs;
using TP1.API.Interfaces;

namespace TP1.API.Data.Repository
{
    public interface IParticipationRepository : IRepository<RequeteParticipationDto, EnvoiParticipationDto>, IListRepository<RequeteParticipationDto>
    {
        bool Exists(int id);
        RequeteParticipationDto GetByIdUnfiltered(int id);
    
    }
}
