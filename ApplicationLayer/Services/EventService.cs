using EventsProject.Application.DTOs;
using EventsProject.Application.Interfaces;
using EventsProject.Domain.Entities;
using EventsProject.Domain.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EventsProject.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IRepository<Event> _eventRepository;
        private readonly IMapper _mapper;

        public EventService(IRepository<Event> eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
        {
            var events = await _eventRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EventDto>>(events);
        }

        public async Task<EventDto> GetEventByIdAsync(int id)
        {
            var @event = await _eventRepository.GetByIdAsync(id);
            return _mapper.Map<EventDto >(@event);
        }

        public async Task<EventDto> GetEventByTitleAsync(string title)
        {
            var @event = (await _eventRepository.GetAllAsync()).FirstOrDefault(e => e.Title == title);
            return _mapper.Map<EventDto>(@event);
        }

        public async Task AddEventAsync(EventDto eventDTO)
        {
            var @event = _mapper.Map<Event>(eventDTO);
            await _eventRepository.AddAsync(@event);
        }

        public async Task UpdateEventAsync(EventDto eventDTO)
        {
            var @event = _mapper.Map<Event>(eventDTO);
            await _eventRepository.UpdateAsync(@event);
        }

        public async Task DeleteEventAsync(int id)
        {
            var @event = await _eventRepository.GetByIdAsync(id);
            await _eventRepository.DeleteAsync(@event);
        }

        public async Task<IEnumerable<EventDto>> GetEventsByCriteriaAsync(DateTime? date, string location, string category)
        {
            var events = await _eventRepository.GetAllAsync();
            if (date.HasValue)
            {
                events = events.Where(e => e.Date.Date == date.Value.Date);
            }
            if (!string.IsNullOrEmpty(location))
            {
                events = events.Where(e => e.Location == location);
            }
            if (!string.IsNullOrEmpty(category))
            {
                events = events.Where(e => e.Category == category);
            }
            return _mapper.Map<IEnumerable<EventDto>>(events);
        }
    }
}
