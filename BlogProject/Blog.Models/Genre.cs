using Blog.Common;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.GenreNameMaxLength)]
        [MinLength(ValidationConstants.GenreNameMinLength)]
        public string Name { get; set; }

        public ICollection<Article> Articles { get; set; } = new List<Article>();
    }
}
