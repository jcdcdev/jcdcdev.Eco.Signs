using System.Text;
using System.Text.RegularExpressions;
using Eco.Gameplay.Economy;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Shared.Utils;
using jcdcdev.Eco.Core.Logging;
using Over9000SignPowerMod.Plugins.Interfaces;

namespace jcdcdev.Eco.Signs.Patterns;

public partial class PersonalBalance : IOver9000SignPowerModPluginTag
{
    public bool CanProcessed(WorldObject worldObject, User registrar) => true;

    public string ProcessTag(Match match, int tagNum, WorldObject worldObject, User registrar)
    {
        var model = PersonalBalanceOptions.Create(match.Groups[1].Value);
        var userName = model.UserName;
        if (string.IsNullOrWhiteSpace(userName))
        {
            return Constants.Errors.NoUserSpecified;
        }

        var user = UserManager.FindUser(userName);
        if (user == null)
        {
            return string.Format(Constants.Errors.UserNotFound, userName);
        }

        var holdings = user.BankAccount.CurrencyHoldings
            .Where(x => !float.IsInfinity(x.Value.Val))
            .Select(x => x.Value)
            .ToList();

        if (holdings.Count == 0)
        {
            return string.Format(Constants.Errors.NoHoldingsForUser, userName);
        }

        CurrencyHolding? holding;
        var currency = model.Currency;
        if (!string.IsNullOrWhiteSpace(currency))
        {
            holding = holdings.FirstOrDefault(x => x.Currency.Name == currency);
        }
        else
        {
            if (holdings.Count > 1)
            {
                var currencies = string.Join(", ", holdings.Select(x => $"<color=yellow>{x.Currency.Name}</color>"));
                return string.Format(Constants.Errors.TooManyHoldingsForUser, userName, currencies);
            }

            holding = holdings.FirstOrDefault();
        }

        if (holding == null)
        {
            return string.Format(Constants.Errors.NoHoldingsForUser, userName);
        }

        var builder = new StringBuilder();
        if (model.ShowName)
        {
            builder.Append($"{holding.Currency.Name}<br>");
        }

        if (model.ShowIcon)
        {
            builder.Append($"<ecoicon item='{holding.Currency.IconName}'>");
        }

        builder.Append(holding.Val.ToString("F2"));

        return builder.ToString();
    }

    public string TagName => "bankAccount";
    public bool Enabled => true;
    public Regex TagRegex => Pattern();

    [GeneratedRegex(@"</bankAccount(.*)>", RegexOptions.Compiled)]
    private static partial Regex Pattern();
}

public class PersonalBalanceOptions
{
    private PersonalBalanceOptions(string? userName, string? currency, bool showIcon, bool showName)
    {
        UserName = userName;
        Currency = currency;
        ShowIcon = showIcon;
        ShowName = showName;
    }

    public string? UserName { get; set; }
    public string? Currency { get; set; }
    public bool ShowIcon { get; set; }
    public bool ShowName { get; set; }

    public static PersonalBalanceOptions Create(string value)
    {
        try
        {
            var segments = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var validFlags = new[] { "i", "n" };
            var flags = segments.Where(x => x.Length == 1 && validFlags.Contains(x)).ToList();
            var others = segments.Except(flags).ToList();
            var userName = others[0];
            var currency = others.Count > 1 ? others[1] : string.Empty;
            var showIcon = flags.ContainsAny("i");
            var showName = flags.ContainsAny("n");
            return new PersonalBalanceOptions(userName, currency, showIcon, showName);
        }
        catch (Exception ex)
        {
            Logger.WriteLine(ex.Message, ConsoleColor.Red);
            return new PersonalBalanceOptions(null, null, false, false);
        }
    }
}