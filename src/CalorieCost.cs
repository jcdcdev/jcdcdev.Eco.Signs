using System.Text.RegularExpressions;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Shared.IoC;
using Eco.Shared.Utils;
using Over9000SignPowerMod.Plugins.Interfaces;

namespace jcdcdev.Eco.Signs;

public class ColorieCost : IOver9000SignPowerModPluginTag
{
    private static DateTime Updated { get; set; }
    private static float? Avg { get; set; }
    public bool CanProcessed(WorldObject worldObject, User registrar) => true;

    public string ProcessTag(Match match, int tagNum, WorldObject worldObject, User registrar)
    {
        UpdateAverage();
        return Avg.GetValueOrDefault(-1).ToString("F2");
    }

    private void UpdateAverage()
    {
        if (Updated >= DateTime.UtcNow - TimeSpan.FromSeconds(10) && Avg.HasValue)
        {
            return;
        }

        var stores = ServiceHolder<IWorldObjectManager>.Obj.All.Where(y => y.HasComponent<StoreComponent>());

        var food = stores.Select(x => x.GetComponent<StoreComponent>()).SelectMany(x =>
                x.AllOffers
                    .Where(y =>
                        !y.Buying
                        && y.Stack.Item.TagNames().Contains("Food")))
            .ToList();

        var costs = new Dictionary<FoodItem, float>();
        foreach (var offer in food.GroupBy(x => x.Stack.Item))
        {
            if (offer.Key is not FoodItem foodItem)
            {
                continue;
            }

            var avgPrice = offer.Average(x => x.Price);
            var calories = foodItem.Calories;

            var costPerCalorie = avgPrice / calories;

            var costPerThousand = costPerCalorie * 1000;

            Log.Debug($"{foodItem.Name} - {calories} cals / {avgPrice} cost");
            costs.Add(foodItem, costPerThousand);
        }
        
        Updated = DateTime.UtcNow;
        Avg = costs.Average(x => x.Value);
    }

    public string TagName => "calorieCost";
    public bool Enabled => true;
    public Regex TagRegex => new Regex("</calorieCost>", RegexOptions.Compiled);
}