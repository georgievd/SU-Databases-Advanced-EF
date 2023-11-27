using System.Text;
using Boardgames.Data.Models;
using Boardgames.Data.Models.Enums;
using Boardgames.DataProcessor.ImportDto;
using Boardgames.Helpers;
using Newtonsoft.Json;

namespace Boardgames.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using Boardgames.Data;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCreator
            = "Successfully imported creator – {0} {1} with {2} boardgames.";

        private const string SuccessfullyImportedSeller
            = "Successfully imported seller - {0} with {1} boardgames.";

        public static string ImportCreators(BoardgamesContext context, string xmlString)
        {
            var creatorDtos = XmlSerializationHelper.Deserialize<ImportCreatorDto[]>(xmlString, "Creators");

            StringBuilder sb = new();

            List<Creator> creatorList = new();

            foreach (var creatorDto in creatorDtos)
            {
                if (!IsValid(creatorDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Creator creator = new()
                {
                    FirstName = creatorDto.FirstName,
                    LastName = creatorDto.LastName
                };

                foreach (var boardGameDto in creatorDto.Boardgames)
                {
                    if (!IsValid(boardGameDto))
                    {
                        sb.AppendLine("Invalid data!");
                        continue;
                    }

                    creator.Boardgames.Add(new Boardgame()
                    {
                        Name = boardGameDto.Name,
                        Rating = boardGameDto.Rating,
                        YearPublished = boardGameDto.YearPublished,
                        CategoryType = (CategoryType)boardGameDto.CategoryType,
                        Mechanics = boardGameDto.Mechanics
                    });
                }

                creatorList.Add(creator);
                sb.AppendLine(string.Format(SuccessfullyImportedCreator, creator.FirstName,
                    creator.LastName, creator.Boardgames.Count()));
            }

            context.Creators.AddRange(creatorList);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportSellers(BoardgamesContext context, string jsonString)
        {
            StringBuilder sb = new();
            var sellerDtos = JsonConvert.DeserializeObject<ImportSellerDto[]>(jsonString);

            List<Seller> sellerList = new ();

            var uniqueBoradgameIds = context.Boardgames
                .Select(bg => bg.Id)
                .ToArray();

            foreach (ImportSellerDto sellerDto in sellerDtos)
            {
                if (!IsValid(sellerDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Seller seller = new()
                {
                    Name = sellerDto.Name,
                    Address = sellerDto.Address,
                    Country = sellerDto.Country,
                    Website = sellerDto.Website
                };

                foreach (var baordgameId in sellerDto.BoardgamesIds.Distinct())
                {
                    if (!uniqueBoradgameIds.Contains(baordgameId))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    BoardgameSeller bgs = new()
                    {
                        Seller = seller,
                        BoardgameId = baordgameId
                    };

                    seller.BoardgamesSellers.Add(bgs);
                }

                sellerList.Add(seller);
                sb.AppendLine(string.Format(SuccessfullyImportedSeller, seller.Name, seller.BoardgamesSellers.Count()));

            }

            context.AddRange(sellerList);
            context.SaveChanges();

            return sb.ToString();   
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
