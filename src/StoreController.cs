using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Shared.IoC;
using Eco.Shared.Utils;

namespace jcdcdev.Eco.Signs;

internal static class StoreController
{
    public static readonly StoreCache Data = new();

    public static void Update()
    {
        var seconds = TimeSpan.FromSeconds(Math.Min(SignsPlugin.Config.StoreUpdateFrequency, 5));
        if (Data.Updated >= DateTime.UtcNow - seconds)
        {
            return;
        }

        var stores = GetAllStores().ToList();
        var forSale = stores.SelectMany(x => x.AllOffers.Where(y => !y.Buying));

        var offers = forSale.GroupBy(x => x.Stack.Item).OrderBy(x => x.Key?.Name);

        Data.AvgCostPerThousandCalories = CalculateCaloriesCost(offers);
        Data.Stores = ProcessStores(stores);
        Data.Updated = DateTime.UtcNow;
    }

    private static Dictionary<Guid, Store> ProcessStores(List<StoreComponent> stores)
    {
        var output = new Dictionary<Guid, Store>();

        foreach (var store in stores)
        {
            var id = store.Parent.ObjectID;
            var selling = store.AllOffers.Where(x => !x.Buying).ToList();
            var buying = store.AllOffers.Where(x => x.Buying).ToList();

            var data = new Store(id, selling, buying);

            output.Add(data.Id, data);
        }

        return output;
    }

    private static float CalculateCaloriesCost(IOrderedEnumerable<IGrouping<Item, TradeOffer>> offers)
    {
        var costs = new Dictionary<Item, float>();
        foreach (var offer in offers.Where(x => x.Any()))
        {
            var item = offer.Key;
            if (item is not FoodItem foodItem)
            {
                continue;
            }

            if (offer.Sum(x => x.Stack.Quantity) == 0)
            {
                continue;
            }

            if (offer.Sum(x => x.Price) == 0)
            {
                costs.Add(item, 0);
                continue;
            }

            var totalQuantity = offer.Sum(x => x.Stack.Quantity);
            var totalPrice = offer.Sum(x => x.Price * x.Stack.Quantity);
            var avgPrice = totalPrice / totalQuantity;
            var costPerCalorie = avgPrice / foodItem.Calories;
            var costPerThousand = costPerCalorie * 1000;

            Log.Debug($"{foodItem.Name} Quantity: {totalQuantity} Cost: {totalPrice} Avg Price: {avgPrice} Cost Per 1000 Cal: {costPerThousand}");
            costs.Add(item, costPerThousand);
        }

        if (!costs.Any())
        {
            return 0;
        }

        if (costs.Sum(x => x.Value) == 0)
        {
            return 0;
        }

        return costs.Average(x => x.Value);
    }

    private static IEnumerable<StoreComponent> GetAllStores()
    {
        var stores = ServiceHolder<IWorldObjectManager>.Obj.All.Where(y => y.HasComponent<StoreComponent>());
        return stores.Select(x => x.GetComponent<StoreComponent>());
    }
}