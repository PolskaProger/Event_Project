using EventsProject.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsProject.Application.Validators
{
    public class EventDTOValidator : AbstractValidator<EventDto>
    {
        public EventDTOValidator()
        {
            RuleFor(e => e.Title).NotEmpty();
            RuleFor(e => e.Description).NotEmpty();
            RuleFor(e => e.Date).NotEmpty();
            RuleFor(e => e.Location).NotEmpty();
            RuleFor(e => e.Category).NotEmpty();
            RuleFor(e => e.MaxParticipants).GreaterThan(0);
        }
    }
}
