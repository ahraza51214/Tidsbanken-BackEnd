using System;
using System.Xml.Linq;
using Tidsbanken_BackEnd.Data.Enums;

namespace Tidsbanken_BackEnd.Data.Entities
{
	public class VacationRequest
	{
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public VacationRequestStatus Status { get; set; }
        public int? UserId { get; set; }
        public int? ApproverId { get; set; }
        public int? StatusId { get; set; }

        // Navigation properties
        public User? User { get; set; } // Makes
        public User? Approver { get; set; }
        public ICollection<Comment>? Comments { get; set; } // Has
    }
}