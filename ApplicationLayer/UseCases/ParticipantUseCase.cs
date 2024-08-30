using AutoMapper;
using EventsProject.Application.DTOs;
using EventsProject.Application.Interfaces;
using EventsProject.Domain.Entities;
using EventsProject.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsProject.Application.UseCases
{
    public class ParticipantUseCase : IParticipantUseCase
    {
        private readonly IRepository<Participant> _participantRepository;
        private readonly IMapper _mapper;

        public ParticipantUseCase(IRepository<Participant> participantRepository, IMapper mapper)
        {
            _participantRepository = participantRepository;
            _mapper = mapper;
        }

        public async Task RegisterParticipantAsync(ParticipantDto participantDTO)
        {
            var participant = _mapper.Map<Participant>(participantDTO);
            await _participantRepository.AddAsync(participant);
        }

        public async Task<IEnumerable<ParticipantDto>> GetParticipantsByEventIdAsync(int eventId)
        {
            var participants = (await _participantRepository.GetAllAsync()).Where(p => p.EventId == eventId);
            return _mapper.Map<IEnumerable<ParticipantDto>>(participants);
        }

        public async Task<ParticipantDto> GetParticipantByIdAsync(int id)
        {
            var participant = await _participantRepository.GetByIdAsync(id);
            return _mapper.Map<ParticipantDto>(participant);
        }

        public async Task UnregisterParticipantAsync(int id)
        {
            var participant = await _participantRepository.GetByIdAsync(id);
            await _participantRepository.DeleteAsync(participant);
        }
    }
}
