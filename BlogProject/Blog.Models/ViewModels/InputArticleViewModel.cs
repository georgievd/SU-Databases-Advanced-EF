using Blog.Common;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models.ViewModels
{
    public class InputArticleViewModel
    {
        [Required]
        [MinLength(ValidationConstants.TitleMinLength)]
        [MaxLength(ValidationConstants.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(ValidationConstants.ContentMinLength)]
        [MaxLength(ValidationConstants.ContentMaxLength)]
        public string Content { get; set; }

        // This is the property that will hold the selected value from the dropdown list
        public int GenreId { get; set; }

        // This is the property that will be used to populate the dropdown list
        public IEnumerable<Genre> Genres { get; set; } = null!;
    }
}
