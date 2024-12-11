using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Orders")]
	public class Order : EntityBase<int>
    {
		public int? UserId { get; set; }
		public decimal TotalAmount { get; set; }
		public DateTime OrderDate { get; set; }
		public string Status { get; set; } = "Pending";
		public string ShippingAddress { get; set; }
		public string PaymentMethod { get; set; }
		public User User { get; set; }
		public ICollection<OrderItem> OrderItems { get; set; }
	}
}
