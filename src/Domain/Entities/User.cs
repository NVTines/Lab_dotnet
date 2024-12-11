using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Users")]
	public class User : EntityBase<int>
    {
        public required string Username { get; set; }
		public required string Password { get; set; }
		public required string Email { get; set; }
		public required string PhoneNumber { get; set; }
		public required string Address { get; set; }
		public bool IsActive { get; set; } = true;
		public ICollection<Order> Orders { get; set; }
		public ICollection<Cart> Carts { get; set; }
	}
}
