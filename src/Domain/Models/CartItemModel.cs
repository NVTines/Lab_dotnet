using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class CartItemModel
    {
        [Key]
        public int Id { get; set; }
        public int CartId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public BookModel? Book { get; set; }
    }
}
