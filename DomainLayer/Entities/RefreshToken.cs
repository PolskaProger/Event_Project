using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsProject.Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
    }
}
