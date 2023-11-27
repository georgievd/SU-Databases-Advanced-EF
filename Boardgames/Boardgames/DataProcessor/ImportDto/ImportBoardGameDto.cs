using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using static Boardgames.Common.ValidationConstants;

namespace Boardgames.DataProcessor.ImportDto
{
    [XmlType("Boardgame")]
    public class ImportBoardGameDto
    {
        [Required]
        [MinLength(BoardGameNameMinLength)]
        [MaxLength(BoardGameNameMaxLength)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Required]
        [Range(BoardgameMinRating, BoardgameMaxRating)]
        [XmlElement("Rating")]
        public double Rating { get; set; }

        [XmlElement("CategoryType")]
        public int CategoryType { get; set; }

        [Required]
        [Range(BoardgameMinYearPublished, BoardgameMaxYearPublished)]
        [XmlElement("YearPublished")]
        public int YearPublished { get; set; }

        [Required]
        [XmlElement("Mechanics")]
        public string Mechanics { get; set; }
    }
}
