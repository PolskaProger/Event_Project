using EventsProject.Domain.Entities;
using EventsProject.Domain.Interfaces;
using EventsProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EventsProject.Infrastructure.Repositories
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly ApplicationDbContext _context;

        public ParticipantRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Participant>> GetAllAsync()
        {
            return await _context.Participants.ToListAsync();
        }

        public async Task<Participant> GetByIdAsync(int id)
        {
            return await _context.Participants.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Participant>> GetByEventIdAsync(int eventId)
        {
            return await _context.Participants
                .Where(p => p.EventId == eventId)
                .ToListAsync();
        }

        public async Task AddAsync(Participant participant)
        {
            await _context.Participants.AddAsync(participant);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Participant participant)
        {
            _context.Participants.Update(participant);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var participant = await _context.Participants.FindAsync(id);
            if (participant != null)
            {
                _context.Participants.Remove(participant);
                await _context.SaveChangesAsync();
            }
        }
    }
}
