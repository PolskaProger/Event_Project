using EventsProject.Application.DTOs;
using EventsProject.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Events_project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetAllEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventDto>> GetEventById(int id)
        {
            var @event = await _eventService.GetEventByIdAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return Ok(@event);
        }

        [HttpGet("title/{title}")]
        public async Task<ActionResult<EventDto>> GetEventByTitle(string title)
        {
            var @event = await _eventService.GetEventByTitleAsync(title);
            if (@event == null)
            {
                return NotFound();
            }
            return Ok(@event);
        }

        [HttpPost]
        public async Task<ActionResult> AddEvent(EventDto eventDTO)
        {
            await _eventService.AddEventAsync(eventDTO);
            return CreatedAtAction(nameof(GetEventById), new { id = eventDTO.Id }, eventDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEvent(int id, EventDto eventDTO)
        {
            if (id != eventDTO.Id)
            {
                return BadRequest();
            }
            await _eventService.UpdateEventAsync(eventDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEvent(int id)
        {
            await _eventService.DeleteEventAsync(id);
            return NoContent();
        }

        [HttpGet("criteria")]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetEventsByCriteria(DateTime? date, string location, string category)
        {
            var events = await _eventService.GetEventsByCriteriaAsync(date, location, category);
            return Ok(events);
        }
    }
}
