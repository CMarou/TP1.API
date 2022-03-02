using TP1.API.Data.Models;
using TP1.API.Interfaces;

namespace TP1.API.Data.Repository
{
    public interface IParticipationRepository : IRepository<Participation>, IListRepository<Participation>
    {
    }
}
