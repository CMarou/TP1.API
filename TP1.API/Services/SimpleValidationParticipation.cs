using TP1.API.Interfaces;
using TP1.API.Models;

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
