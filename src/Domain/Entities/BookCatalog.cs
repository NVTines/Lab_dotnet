using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("BookCatalogs")]
    public class BookCatalog : EntityBase<int>
    {
		public int BookId { get; set; }
		public Book Book { get; set; }
		public int CatalogId { get; set; }
		public Catalog Catalog { get; set; }
	}
}