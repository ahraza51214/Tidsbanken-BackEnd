using System;
using System.ComponentModel.DataAnnotations;
using Tidsbanken_BackEnd.Data.Enums;

namespace Tidsbanken_BackEnd.Data.DTOs.CommentDTOs
{
	public class CommentPutDTO
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
        public required int RequestId { get; set; } // Non-nullable for mandatory association with a vacation request
    }
}