using TP1.API.Data.Models;
using TP1.API.Interfaces;

namespace TP1.API.Services
{
    public class SimpleValidationParticipation : IValidationParticipation
    {
        public bool Validate(Participation participation)
        {
            participation.EstValide = true;
            return true;
        }
    }
}
