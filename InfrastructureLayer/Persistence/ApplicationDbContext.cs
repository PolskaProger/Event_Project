using Microsoft.EntityFrameworkCore;
using EventsProject.Domain.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace EventsProject.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {   
        }
        public DbSet<Event> Events { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
