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
    public class ParticipantsController : ControllerBase
    {
        private readonly IParticipantUseCase _participantUseCase;

        public ParticipantsController(IParticipantUseCase participantUseCase)
        {
            _participantUseCase = participantUseCase;
        }

        [HttpPost]
        public async Task<ActionResult> RegisterParticipant(ParticipantDto participantDTO)
        {
            await _participantUseCase.RegisterParticipantAsync(participantDTO);
            return CreatedAtAction(nameof(GetParticipantById), new { id = participantDTO.Id }, participantDTO);
        }

        [HttpGet("event/{eventId}")]
        [Authorize(Policy = "ParticipantPolicy")]
        public async Task<ActionResult<IEnumerable<ParticipantDto>>> GetParticipantsByEventId(int eventId)
        {
            var participants = await _participantUseCase.GetParticipantsByEventIdAsync(eventId);
            return Ok(participants);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ParticipantPolicy")]
        public async Task<ActionResult<ParticipantDto>> GetParticipantById(int id)
        {
            var participant = await _participantUseCase.GetParticipantByIdAsync(id);
            if (participant == null)
            {
                return NotFound();
            }
            return Ok(participant);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "ParticipantPolicy")]
        public async Task<ActionResult> UnregisterParticipant(int id)
        {
            await _participantUseCase.UnregisterParticipantAsync(id);
            return NoContent();
        }
    }
}
