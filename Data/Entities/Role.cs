using System;
namespace Tidsbanken_BackEnd.Data.Entities
{
	public class Role
	{
        public int Id { get; set; }
        public string RoleName { get; set; }

        // Navigation properties
        public ICollection<User> Users { get; set; } // Has
    }
}