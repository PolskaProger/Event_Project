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
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn();
            builder.Property(e => e.Title).IsRequired();
            builder.Property(e => e.Description).IsRequired();
            builder.Property(e => e.Date).IsRequired();
            builder.Property(e => e.Location).IsRequired();
            builder.Property(e => e.Category).IsRequired();
            builder.Property(e => e.MaxParticipants).IsRequired();
            builder.HasMany(e => e.Participants).WithOne(p => p.Event).HasForeignKey(p => p.EventId);
        }
    }
}
