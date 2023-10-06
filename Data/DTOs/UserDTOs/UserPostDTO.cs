﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tidsbanken_BackEnd.Data.DTOs.UserDTOs
{
	public class UserPostDTO
	{
        [Required]
        [MaxLength(50)] // Limits the maximum length of the string property to 50 characters
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public required string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Password { get; set; } // Consider using a more secure method for storing passwords

        [Required]
        [MaxLength(100)]
        [EmailAddress] // Ensures that the Email property is a valid email address
        public required string Email { get; set; }

        [Required]
        [ForeignKey("Role")]
        public int RoleId { get; set; } // Non-nullable for mandatory role associatione
    }
}