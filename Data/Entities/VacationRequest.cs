using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tidsbanken_BackEnd.Data.Enums;

namespace Tidsbanken_BackEnd.Data.Entities
{
    public class VacationRequest
    {
        public int Id { get; set; }

        [Required]
        public VacationType VacationType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public VacationRequestStatus Status { get; set; }

        [Required]
        [ForeignKey("User")]
        public required Guid UserId { get; set; } // Nullable for optional user

        [Required]
        public DateTime RequestDate { get; set; }

        [ForeignKey("Approver")]
        public Guid? ApproverId { get; set; } // Nullable for optional approver

        public DateTime? ApprovalDate { get; set; }

        // Navigation properties
        public User? User { get; set; }
        public User? Approver { get; set; }
        public ICollection<Comment>? Comments { get; set; }


        public VacationRequest()
        {
            // Set the default Status to Pending when a vacation request is craeted (index 0)
            Status = VacationRequestStatus.Pending;
        }
    }
}