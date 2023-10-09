using System;
using Tidsbanken_BackEnd.Data.Enums;
using System.ComponentModel.DataAnnotations;


namespace Tidsbanken_BackEnd.Data.DTOs.VacationRequestDTOs
{
	public class VacationRequestDTO
	{
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public VacationRequestStatus Status { get; set; }

        [Required]
        public required int UserId { get; set; }

        [Required]
        public DateTime RequestDate { get; set; }

        public int? ApproverId { get; set; } 

        public DateTime? ApprovalDate { get; set; }
    }
}