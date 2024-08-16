using EventsProject.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsProject.Application.Interfaces
{
    public interface IParticipantService
    {
        Task RegisterParticipantAsync(ParticipantDto participantDTO);
        Task<IEnumerable<ParticipantDto>> GetParticipantsByEventIdAsync(int eventId);
        Task<ParticipantDto> GetParticipantByIdAsync(int id);
        Task UnregisterParticipantAsync(int id);
    }
}
