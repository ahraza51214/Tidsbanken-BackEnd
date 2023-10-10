using System;
using System.ComponentModel.DataAnnotations;
using Tidsbanken_BackEnd.Data.Enums;

namespace Tidsbanken_BackEnd.Data.DTOs.CommentDTOs
{
	public class CommentPostDTO
	{
        [Required]
        [MaxLength(1000)] // Limits the maximum length of the string property to 1000 characters
        public required string Message { get; set; }

        [Required]
        public required DateTime DateCommented { get; set; }

        [Required]
        public required string StatusAtTimeOfComment { get; set; }

        [Required]
        public required int RequestId { get; set; } // Non-nullable for mandatory association with a vacation request
    }
}