using System.ComponentModel.DataAnnotations;

using static Boardgames.Common.ValidationConstants;

namespace Boardgames.Data.Models
{
    public class Creator
    {
        public Creator()
        {
            Boardgames = new List<Boardgame>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CreatorFirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(CreatorLastNameMaxLength)]
        public string LastName { get; set; }

        public virtual ICollection<Boardgame> Boardgames { get; set; }
    }
}
