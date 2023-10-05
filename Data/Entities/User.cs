using System;
using System.Xml.Linq;

namespace Tidsbanken_BackEnd.Data.Entities
{
	public class User
	{
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // Consider using a more secure type or method for storing passwords
        public string Email { get; set; }
        public int? RoleId { get; set; }

        // Navigation properties
        public Role? Role { get; set; }
        public ICollection<VacationRequest> VacationRequests { get; set; } // Makes
        public ICollection<IneligiblePeriod> IneligiblePeriods { get; set; } // Has
        public ICollection<Comment> Comments { get; set; } // Writes
    }
}