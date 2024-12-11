using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public required string Username { get; set; }
        public string Password { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Order> Orders { get; set; }
        public ICollection<CartModel> Carts { get; set; }
    }
}
