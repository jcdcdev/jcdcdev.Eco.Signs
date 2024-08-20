using System.Text.RegularExpressions;

namespace jcdcdev.Eco.Signs;

internal static partial class Constants
{
    public static partial class RegexPatterns
    {
        public static readonly Regex Guid = GuidRegex();

        [GeneratedRegex("[a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12}", RegexOptions.Compiled)]
        private static partial Regex GuidRegex();
    }

    public static class Errors
    {
        public const string StoreInvalid = "<color=red>Error</color><br>Failed to extract store id";
        public const string StoreNotFound = "<color=red>Error</color><br>Store cannot be found";
        public const string UserNotFound = "<color=red>Error</color><br>User {0} cannot be found";
        public const string NoHoldingsForUser = "<color=red>Error</color><br>No holdings can be found for {0}";
        public const string TooManyHoldingsForUser = "<color=red>Error</color><br>More than one holding found for {0}<br>Please specify currency: {1}";
        public const string NoUserSpecified = "<color=red>Error</color><br>No user specified";
    }
}