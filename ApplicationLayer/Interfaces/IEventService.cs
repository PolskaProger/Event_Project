using EventsProject.Application.DTOs;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsProject.Application.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventDto>> GetAllEventsAsync();
        Task<EventDto> GetEventByIdAsync(int id);
        Task<EventDto> GetEventByTitleAsync(string title);
        Task AddEventAsync(EventDto eventDTO);
        Task UpdateEventAsync(EventDto eventDTO);
        Task DeleteEventAsync(int id);
        Task<IEnumerable<EventDto>> GetEventsByCriteriaAsync(DateTime? date, string location, string category);
    }
}
