using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class OrderItemModel
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public OrderModel Order { get; set; }
        public BookModel Book { get; set; }
    }
}
