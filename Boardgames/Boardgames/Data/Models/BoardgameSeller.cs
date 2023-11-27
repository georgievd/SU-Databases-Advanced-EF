using System.ComponentModel.DataAnnotations.Schema;

namespace Boardgames.Data.Models
{
    public class BoardgameSeller
    {
        public int BoardgameId { get; set; }
        [ForeignKey(nameof(BoardgameId))]
        public virtual Boardgame Boardgame { get; set; }

        public int SellerId { get; set; }
        [ForeignKey(nameof(SellerId))]
        public virtual Seller Seller { get; set; }
    }
}
