using System;
namespace Tidsbanken_BackEnd.Data.Entities
{
	public class IneligiblePeriod
	{
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }

        // Navigation properties
        public User User { get; set; } // Has
    }
}