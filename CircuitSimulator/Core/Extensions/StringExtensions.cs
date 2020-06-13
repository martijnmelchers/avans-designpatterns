namespace Core.Extensions
{
    public static class StringExtensions
    {
        public static bool InvalidEntry(this string input)
        {
            return !input.Contains(":") && !string.IsNullOrWhiteSpace(input) && !input.StartsWith("#");
        }
        public static string[] ParseLine(this string input)
        {
            return input.ToLower()
                .Replace("\t", "")
                .Replace(" ", "")
                .Replace(";", "")
                .Split(":");
        }
    }
}