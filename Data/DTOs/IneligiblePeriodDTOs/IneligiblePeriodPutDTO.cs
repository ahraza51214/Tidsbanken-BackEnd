using System;
using System.ComponentModel.DataAnnotations;

namespace Tidsbanken_BackEnd.Data.DTOs.IneligiblePeriodDTOs
{
	public class IneligiblePeriodPutDTO
	{
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [MaxLength(255)] // Limits the maximum length of the string property to 255 characters
        public string? Description { get; set; } // Nullable if optional

        [Required]
        public Guid UserId { get; set; }
    }
}