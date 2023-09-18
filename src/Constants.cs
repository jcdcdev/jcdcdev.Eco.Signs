using System.Text.RegularExpressions;

namespace jcdcdev.Eco.Signs;

internal static class Constants
{
    public static class RegexPatterns
    {
        public static Regex Guid =
            new Regex(@"[a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12}",
                RegexOptions.Compiled);
    }
}