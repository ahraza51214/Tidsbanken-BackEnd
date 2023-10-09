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
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public VacationRequestStatus Status { get; set; }

        [Required]
        public required int UserId { get; set; } // Nullable for optional user

        [Required]
        public DateTime RequestDate { get; set; }

        public int? ApproverId { get; set; } // Nullable for optional approver

        public DateTime? ApprovalDate { get; set; }

        // Navigation properties
        public required User User { get; set; }
        public required User Approver { get; set; }
        public required ICollection<Comment> Comments { get; set; }
    }
}