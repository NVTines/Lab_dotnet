using Domain.Entities;
using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class BookModel
	{
		[Key]	
		public int Id { get; set; }

		[Required(ErrorMessage = "Title is required")]
		[StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Author is required")]
		[StringLength(100, ErrorMessage = "Author name cannot exceed 100 characters")]
		public string Author { get; set; }

		public int Quantity { get; set; }

		public bool Available { get; set; } = true;

        [StringLength(100, ErrorMessage = "Publisher name cannot exceed 100 characters")]
		public string Publisher { get; set; }

		[Range(0.01, 10000.00, ErrorMessage = "Price must be between 0.01 and 10000.00")]
		public decimal Price { get; set; }

		public DateTime? CreatedOn { get; set; }

		public int? GenreId { get; set; }

		public bool IsActive { get; set; } = true;

		public GenreModel Genre { get; set; }

		public ICollection<BookCatalog> BookCatalogs { get; set; } = new List<BookCatalog>();
	}
}
