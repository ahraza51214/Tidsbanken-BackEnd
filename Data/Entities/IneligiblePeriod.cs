using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tidsbanken_BackEnd.Data.Entities
{
    public class IneligiblePeriod
    {
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [MaxLength(255)] // Limits the maximum length of the string property to 255 characters
        public string? Description { get; set; } // Nullable if optional

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        // Navigation properties
        public User? User { get; set; }
    }
}