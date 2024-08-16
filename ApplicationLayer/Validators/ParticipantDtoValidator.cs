using EventsProject.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsProject.Application.Validators
{
    public class ParticipantDTOValidator : AbstractValidator<ParticipantDto>
    {
        public ParticipantDTOValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty();
            RuleFor(p => p.LastName).NotEmpty();
            RuleFor(p => p.BirthDate).NotEmpty();
            RuleFor(p => p.RegistrationDate).NotEmpty();
            RuleFor(p => p.Email).NotEmpty().EmailAddress();
            RuleFor(p => p.EventId).GreaterThan(0);
        }
    }
}
