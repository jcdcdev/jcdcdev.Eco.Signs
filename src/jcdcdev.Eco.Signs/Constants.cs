using System.Text.RegularExpressions;

namespace jcdcdev.Eco.Signs;

internal static class Constants
{
    public static class RegexPatterns
    {
        public static readonly Regex Guid =
            new(@"[a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12}",
                RegexOptions.Compiled);
    }

    public static class Errors
    {
        public const string StoreInvalid = "<color=red>Error</color><br>Failed to extract store id";
        public const string StoreNotFound = "<color=red>Error</color><br>Store cannot be found";
    }
}