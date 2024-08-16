using EventsProject.Application.DTOs;
using EventsProject.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Events_project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantsController : ControllerBase
    {
        private readonly IParticipantService _participantService;

        public ParticipantsController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        [HttpPost]
        public async Task<ActionResult> RegisterParticipant(ParticipantDto participantDTO)
        {
            await _participantService.RegisterParticipantAsync(participantDTO);
            return CreatedAtAction(nameof(GetParticipantById), new { id = participantDTO.Id }, participantDTO);
        }

        [HttpGet("event/{eventId}")]
        public async Task<ActionResult<IEnumerable<ParticipantDto>>> GetParticipantsByEventId(int eventId)
        {
            var participants = await _participantService.GetParticipantsByEventIdAsync(eventId);
            return Ok(participants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParticipantDto>> GetParticipantById(int id)
        {
            var participant = await _participantService.GetParticipantByIdAsync(id);
            if (participant == null)
            {
                return NotFound();
            }
            return Ok(participant);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> UnregisterParticipant(int id)
        {
            await _participantService.UnregisterParticipantAsync(id);
            return NoContent();
        }
    }
}
