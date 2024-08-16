using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsProject.Domain.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public int MaxParticipants { get; set; }
        public ICollection<Participant> Participants { get; set; }
        public string ImageUrl { get; set; }
    }

}
