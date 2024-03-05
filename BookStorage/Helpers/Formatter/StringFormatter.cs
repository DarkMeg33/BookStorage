namespace BookStorage.Helpers.Formatter
{
    public static class StringFormatter
    {
        public static string GetFormattedFileSize(int sizeInBytes)
        {
            double sizeInKb = sizeInBytes / 1024.0;

            if (sizeInKb > 1024)
            {
                return $"{sizeInKb / 1024}MB";
            }

            return $"{sizeInKb}KB";
        }

        public static string FormatName(string firstname, string lastname, string separator = " ")
        {
            return $"{firstname}{separator}{lastname}";
        }

        public static string ToBookCoverUrl(string storageReference)
        {
            if (string.IsNullOrWhiteSpace(storageReference))
            {
                return Constants.Constants.DefaultCoverUrl;
            }

            return $"/book-cover/{storageReference}";
        }
    }
}