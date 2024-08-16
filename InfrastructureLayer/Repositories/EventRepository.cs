// EventsProject.Infrastructure/Repositories/EventRepository.cs
using EventsProject.Domain.Entities;
using EventsProject.Domain.Interfaces;
using EventsProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EventsProject.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _context.Events.Include(e => e.Participants).ToListAsync();
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            return await _context.Events.Include(e => e.Participants).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Event> GetByNameAsync(string name)
        {
            return await _context.Events.Include(e => e.Participants).FirstOrDefaultAsync(e => e.Title == name);
        }

        public async Task<IEnumerable<Event>> GetByCriteriaAsync(DateTime? date, string location, string category)
        {
            var query = _context.Events.Include(e => e.Participants).AsQueryable();

            if (date.HasValue)
            {
                query = query.Where(e => e.Date.Date == date.Value.Date);
            }

            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(e => e.Location.Contains(location));
            }

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(e => e.Category.Contains(category));
            }

            return await query.ToListAsync();
        }

        public async Task AddAsync(Event eventEntity)
        {
            await _context.Events.AddAsync(eventEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Event eventEntity)
        {
            _context.Events.Update(eventEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var eventEntity = await _context.Events.FindAsync(id);
            if (eventEntity != null)
            {
                _context.Events.Remove(eventEntity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
