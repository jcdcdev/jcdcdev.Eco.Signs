using System.Text.RegularExpressions;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Over9000SignPowerMod.Plugins.Interfaces;

namespace jcdcdev.Eco.Signs.Patterns;

public class CalorieCost : IOver9000SignPowerModPluginTag
{
    public bool CanProcessed(WorldObject worldObject, User registrar) => true;

    public string ProcessTag(Match match, int tagNum, WorldObject worldObject, User registrar)
    {
        var avg = StoreController.Data.AvgCostPerThousandCalories;
        return avg.GetValueOrDefault(-1).ToString("F2");
    }
    
    public string TagName => "calorieCost";
    public bool Enabled => true;
    public Regex TagRegex => new("</calorieCost>", RegexOptions.Compiled);
}