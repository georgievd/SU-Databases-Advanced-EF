using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using Newtonsoft.Json;
using static Boardgames.Common.ValidationConstants;

namespace Boardgames.DataProcessor.ImportDto
{
    public class ImportSellerDto
    {
        [Required]
        [MinLength(SellerNameMinLength)]
        [MaxLength(SellerNameMaxLength)]
        [JsonProperty("Name")]
        public string Name { get; set; }

        [Required]
        [MinLength(SellerAddressMinLength)]
        [MaxLength(SellerAddressMaxLength)]
        [JsonProperty("Address")]
        public string Address { get; set; }

        [Required]
        [JsonProperty("Country")]
        public string Country { get; set; }

        [Required]
        [RegularExpression(WebsiteRegEx)]
        [JsonProperty("Website")]
        public string Website { get; set; }

        [JsonProperty("Boardgames")]
        public int[] BoardgamesIds { get; set; }

    }
}
