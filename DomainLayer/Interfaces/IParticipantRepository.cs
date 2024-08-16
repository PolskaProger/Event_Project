using EventsProject.Domain.Entities;

namespace EventsProject.Domain.Interfaces
{
    public interface IParticipantRepository
    {
        Task<IEnumerable<Participant>> GetAllAsync();
        Task<Participant> GetByIdAsync(int id);
        Task<IEnumerable<Participant>> GetByEventIdAsync(int eventId);
        Task AddAsync(Participant participant);
        Task UpdateAsync(Participant participant);
        Task DeleteAsync(int id);
    }
}
