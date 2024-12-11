using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("CartItems")]
	public class CartItem : EntityBase<int>
    {
		public int CartId { get; set; }
		public int BookId { get; set; }
		public int Quantity { get; set; }
		public Cart Cart { get; set; }
		public Book Book { get; set; }
	}
}
