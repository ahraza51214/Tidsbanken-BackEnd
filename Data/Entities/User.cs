using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Tidsbanken_BackEnd.Data.Entities
{
    public class User
    {
        [Required]
        [Key]
        public required Guid Id { get; set; }
           
        [MaxLength(100)]
        public required string Username { get; set; }
 
        [MaxLength(50)] // Limits the maximum length of the string property to 50 characters    
        public required string FirstName { get; set; }

        [MaxLength(50)]
        public required string LastName { get; set; }

        [MaxLength(100)]
        [EmailAddress] // Ensures that the Email property is a valid email address
        public required string Email { get; set; }

        // Navigation properties
        public ICollection<VacationRequest>? VacationRequests { get; set; }
        public ICollection<IneligiblePeriod>? IneligiblePeriods { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}