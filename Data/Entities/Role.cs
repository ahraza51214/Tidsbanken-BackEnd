using System;
using System.ComponentModel.DataAnnotations;

namespace Tidsbanken_BackEnd.Data.Entities
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)] // Limits the maximum length of the string property to 50 characters
        public required string RoleName { get; set; }

        // Navigation properties
        public ICollection<User>? Users { get; set; }
    }
}