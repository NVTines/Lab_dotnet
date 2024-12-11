using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Catalogs")]

    public class Catalog : EntityBase<int>
    {
		public string Title { get; set; }
		public string Description { get; set; }
		public bool IsActive { get; set; } = true;
		public ICollection<BookCatalog> BookCatalogs { get; set; } = new List<BookCatalog>();
	}
}
