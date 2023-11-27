using System.ComponentModel.DataAnnotations;
using static Boardgames.Common.ValidationConstants;

namespace Boardgames.Data.Models
{
    public class Seller
    {
        public Seller()
        {
            BoardgamesSellers = new List<BoardgameSeller>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(SellerNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(SellerAddressMaxLength)]
        public string Address { get; set; }

        [Required] 
        public string Country { get; set; }

        [Required]
        public string Website { get; set; }

        public virtual ICollection<BoardgameSeller> BoardgamesSellers { get; set; }
    }
}
