namespace DataManagement
{
    /// <summary>
    /// Some conversion methods.
    /// </summary>
    public static class MovieExtention {
        /// <summary>
        /// Returns friendly strings for a movie classification
        /// </summary>
        /// <param name="m">the movie classification to friendlify</param>
        /// <returns>[long name, code]</returns>
        public static string[] FriendlyClassName(this MovieClass m)
        {
            switch((int)m)
            {
                case 0:
                    return new string[] { "General", "G" };
                case 1:
                    return new string[] { "Parental Guidance", "PG" };
                case 2:
                    return new string[] { "Mature", "M15+" };
                case 3:
                    return new string[] { "Mature Accompanied", "MA15+" };
                default:
                    return new string[] { "Unknown", "NA" };
            }
        }
        /// <summary>
        /// casts the string Classification name, to the MovieClass enum.
        /// </summary>
        /// <param name="m">The string to cast</param>
        /// <returns>The appropriate MovieClass</returns>
        public static MovieClass FriendlyClassName(this string m)
        {
            switch (m)
            {
                case "General":
                case "G":
                    return (MovieClass)0;
                case "Parental Guidance":
                case "PG":
                    return (MovieClass)1;
                case "Mature":
                case "M15+":
                    return (MovieClass)2;
                case "Mature Accompanied":
                case "MA15+":
                    return (MovieClass)3;
                default:
                    return (MovieClass)4;
            }
        }

        /// <summary>
        /// Returns friendly strings for a movie genre
        /// </summary>
        /// <param name="m">the movie classification to friendlify</param>
        /// <returns>the genre name</returns>
        public static string FriendlyGenreName(this MovieGenre m)
        {
            switch ((int)m)
            {
                case 0:
                    return "Drama";
                case 1:
                    return "Adventure";
                case 2:
                    return "Family";
                case 3:
                    return "Action";
                case 4:
                    return "Sci-Fi";
                case 5:
                    return "Comedy";
                case 6:
                    return "Animated";
                case 7:
                    return "Thriller";
                case 8:
                    return "Other";
                default:
                    return "Unknown";
            }
        }
        /// <summary>
        /// Casts the string Movie Genre to the MoveGenre type
        /// </summary>
        /// <param name="m">the string to cast</param>
        /// <returns>The appropriate MovieGenre</returns>
        public static MovieGenre FriendlyGenreName(this string m)
        {
            switch (m)
            {
                case "Drama":
                    return (MovieGenre)0;
                case "Adventure":
                    return (MovieGenre)1;
                case "Family":
                    return (MovieGenre)2;
                case "Action":
                    return (MovieGenre)3;
                case "Sci-Fi":
                    return (MovieGenre)4;
                case "Comedy":
                    return (MovieGenre)5;
                case "Animated":
                    return (MovieGenre)6;
                case "Thriller":
                    return (MovieGenre)7;
                case "Other":
                    return (MovieGenre)8;
                default:
                    return (MovieGenre)9;
            }
        }
    }
}
