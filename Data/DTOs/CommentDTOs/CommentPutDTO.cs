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

    }
}