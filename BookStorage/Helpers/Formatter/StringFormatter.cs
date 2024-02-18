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
    }
}