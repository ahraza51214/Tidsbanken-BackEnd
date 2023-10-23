using System;
using System.ComponentModel.DataAnnotations;

namespace Tidsbanken_BackEnd.Data.DTOs.VacationRequestDTOs
{
	public class VacationRequestPostDTO
	{
        [Required]
        public required string VacationType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public required Guid UserId { get; set; }

        [Required]
        public DateTime RequestDate { get; set; }
    }
}