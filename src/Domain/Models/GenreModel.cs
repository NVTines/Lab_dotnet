using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class GenreModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(1000, ErrorMessage = "Name cannot exceed 1000 characters")]
        public string Description { get; set; }
    }
}
