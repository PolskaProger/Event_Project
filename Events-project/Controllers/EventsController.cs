using EventsProject.Application.DTOs;
using EventsProject.Application.Interfaces;
using EventsProject.Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Events_project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "ParticipantPolicy")]
    public class EventsController : ControllerBase
    {
        private readonly IEventUseCase _eventUseCase;

        public EventsController(IEventUseCase eventUseCase)
        {
            _eventUseCase = eventUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetAllEvents()
        {
            var events = await _eventUseCase.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventDto>> GetEventById(int id)
        {
            var @event = await _eventUseCase.GetEventByIdAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return Ok(@event);
        }

        [HttpGet("title/{title}")]
        public async Task<ActionResult<EventDto>> GetEventByTitle(string title)
        {
            var @event = await _eventUseCase.GetEventByTitleAsync(title);
            if (@event == null)
            {
                return NotFound();
            }
            return Ok(@event);
        }

        [HttpPost]
        public async Task<ActionResult> AddEvent(EventDto eventDTO)
        {
            await _eventUseCase.AddEventAsync(eventDTO);
            return CreatedAtAction(nameof(GetEventById), new { id = eventDTO.Id }, eventDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEvent(int id, EventDto eventDTO)
        {
            if (id != eventDTO.Id)
            {
                return BadRequest();
            }
            await _eventUseCase.UpdateEventAsync(eventDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEvent(int id)
        {
            await _eventUseCase.DeleteEventAsync(id);
            return NoContent();
        }

        [HttpGet("criteria")]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetEventsByCriteria(DateTime? date, string location, string category)
        {
            var events = await _eventUseCase.GetEventsByCriteriaAsync(date, location, category);
            return Ok(events);
        }
    }
}
