using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class CartModel
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public UserModel? User { get; set; }
        public ICollection<CartItemModel> CartItems { get; set; } = new List<CartItemModel>();
    }
}
