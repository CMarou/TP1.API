using TP1.API.DTOs;
using TP1.API.Interfaces;

namespace TP1.API.Services
{
    public class MockValidationParticipation : IValidationParticipation
    {
        public bool Validate(int participationId)
        {
            return true;
        }
    }
}
