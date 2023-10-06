using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tidsbanken_BackEnd.Data.Enums;

namespace Tidsbanken_BackEnd.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(1000)] // Limits the maximum length of the string property to 1000 characters
        public required string Message { get; set; }

        [Required]
        public required DateTime DateCommented { get; set; }

        [Required]
        [MaxLength(50)] // Limits the maximum length of the string property to 50 characters
        public required string CommentType { get; set; }

        [Required]
        public required VacationRequestStatus StatusAtTimeOfComment { get; set; }

        [Required]
        [ForeignKey("VacationRequest")]
        public required int RequestId { get; set; } // Non-nullable for mandatory association with a vacation request

        // Navigation properties
        public required VacationRequest VacationRequest { get; set; }
    }
}