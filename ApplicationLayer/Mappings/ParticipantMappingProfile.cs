using AutoMapper;
using EventsProject.Application.DTOs;
using EventsProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsProject.Application.Mappings
{
    public class ParticipantMappingProfile : Profile
    {
        public ParticipantMappingProfile()
        {
            CreateMap<Participant, ParticipantDto>().ReverseMap();
        }
    }
}
