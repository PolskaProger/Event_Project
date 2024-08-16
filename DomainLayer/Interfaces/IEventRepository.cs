using EventsProject.Domain.Entities;
using EventsProject.Application.DTOs;

namespace EventsProject.Domain.Interfaces
{

    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllAsync(PaginationFilter filter);
        Task<Event> GetByIdAsync(int id);
        Task<Event> GetByNameAsync(string name);
        Task<IEnumerable<Event>> GetByCriteriaAsync(DateTime? date, string location, string category);
        Task AddAsync(Event eventEntity);
        Task UpdateAsync(Event eventEntity);
        Task DeleteAsync(int id);
    }
}
