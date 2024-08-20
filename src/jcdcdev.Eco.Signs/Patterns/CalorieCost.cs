using System.Text.RegularExpressions;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using jcdcdev.Eco.Core.Services;
using Over9000SignPowerMod.Plugins.Interfaces;

namespace jcdcdev.Eco.Signs.Patterns;

public partial class CalorieCost : IOver9000SignPowerModPluginTag
{
    public bool CanProcessed(WorldObject worldObject, User registrar) => true;

    public string ProcessTag(Match match, int tagNum, WorldObject worldObject, User registrar)
    {
        var oos = match.Value.Contains("oos");
        var id = Constants.RegexPatterns.Guid.Match(match.Value);
        float? avg;

        if (Guid.TryParse(id.Value, out var guid))
        {
            var store = StoreService.Data.Get(guid);
            if (store == null)
            {
                return Constants.Errors.StoreNotFound;
            }

            avg = oos ? store.AvgCostPerThousandCalories : store.AvgCostPerThousandCaloriesInStock;
        }
        else
        {
            avg = oos
                ? StoreService.Data.AvgCostPerThousandCalories
                : StoreService.Data.AvgCostPerThousandCaloriesInStock;
        }

        return avg.GetValueOrDefault(-1).ToString("F2");
    }

    public string TagName => "calorieCost";
    public bool Enabled => true;
    public Regex TagRegex => Pattern();

    [GeneratedRegex("</calorieCost(.*?)>", RegexOptions.Compiled)]
    private static partial Regex Pattern();
}