using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Books")]
    public class Book: EntityBase<int>
	{
		public string Title { get; set; }
		public string Author { get; set; }
		public int Quantity { get; set; }
		public bool Available { get; set; } = true;
        public string Publisher { get; set; }
		public decimal Price { get; set; }
		public DateTime? CreatedOn { get; set; }
		public int? GenreId { get; set; }
        public bool IsActive { get; set; } = true;
		public Genre Genre { get; set; }
		public ICollection<BookCatalog> BookCatalogs { get; set; } = new List<BookCatalog>();
	}
}
