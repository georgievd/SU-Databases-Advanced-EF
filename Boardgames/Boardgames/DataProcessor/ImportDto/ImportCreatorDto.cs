using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using static Boardgames.Common.ValidationConstants;

namespace Boardgames.DataProcessor.ImportDto
{
    [XmlType("Creator")]
    public class ImportCreatorDto
    {
        [Required]
        [MinLength(CreatorFirstNameMinLength)]
        [MaxLength(CreatorFirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(CreatorLastNameMinLength)]
        [MaxLength(CreatorLastNameMaxLength)]
        public string LastName { get; set; }

        [XmlArray("Boardgames")]
        public ImportBoardGameDto[] Boardgames { get; set; }
    }
}
