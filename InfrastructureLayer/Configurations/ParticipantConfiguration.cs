using EventsProject.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsProject.Infrastructure.Configurations
{
    public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.FirstName).IsRequired();
            builder.Property(p => p.LastName).IsRequired();
            builder.Property(p => p.BirthDate).IsRequired();
            builder.Property(p => p.RegistrationDate).IsRequired();
            builder.Property(p => p.Email).IsRequired();
            builder.HasOne(p => p.Event).WithMany(e => e.Participants).HasForeignKey(p => p.EventId);
        }
    }
}
