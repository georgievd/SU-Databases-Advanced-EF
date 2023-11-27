namespace Boardgames.Common
{
    public static class ValidationConstants
    {
        // Boardgame
        public const int BoardGameNameMinLength = 10;
        public const int BoardGameNameMaxLength = 20;

        public const double BoardgameMinRating = 1;
        public const double BoardgameMaxRating = 10;

        public const int BoardgameMinYearPublished = 2018;
        public const int BoardgameMaxYearPublished = 2023;


        // Seller
        public const int SellerNameMinLength = 5;
        public const int SellerNameMaxLength = 20;

        public const int SellerAddressMinLength = 2;
        public const int SellerAddressMaxLength = 30;

        public const string WebsiteRegEx = @"^www\.[a-zA-Z0-9-]+\.com";



        // Creator 
        public const int CreatorFirstNameMinLength = 2;
        public const int CreatorFirstNameMaxLength = 7;

        public const int CreatorLastNameMinLength = 2;
        public const int CreatorLastNameMaxLength = 7;

    }
}
