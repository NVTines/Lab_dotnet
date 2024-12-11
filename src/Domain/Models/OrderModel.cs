using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class OrderModel
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = "Pending";
        public string ShippingAddress { get; set; }
        public string PaymentMethod { get; set; }
        public UserModel User { get; set; }
        public ICollection<OrderItemModel> OrderItems { get; set; }
    }
}
