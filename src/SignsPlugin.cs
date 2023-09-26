using System.Globalization;
using Eco.Shared.Localization;
using jcdcdev.Eco.Core;
using Over9000SignPowerMod.Controllers;

namespace jcdcdev.Eco.Signs;

public class SignsPlugin : PluginBase<SignConfig>
{
    protected override void RunMod()
    {
        while (Active)
        {
            StoreController.Update();
            Thread.Sleep((int)Math.Max(250, 1000 - MainController.LastTickTime));
        }
    }

    protected override void BuildStatusText(LocStringBuilder sb)
    {
        sb.AppendLineNTStr($"Updated :{StoreController.Data.Updated.ToString(CultureInfo.InvariantCulture)}");
        sb.AppendLineNTStr($"Avg Cost 1000 Calories :{StoreController.Data.AvgCostPerThousandCalories}");
        sb.AppendLineNTStr($"Store Count :{StoreController.Data.Stores.Count}");
        base.BuildStatusText(sb);
    }
}