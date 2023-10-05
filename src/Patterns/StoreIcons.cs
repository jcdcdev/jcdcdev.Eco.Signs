using System.Collections.Concurrent;
using System.Text;
using System.Text.RegularExpressions;
using Eco.Gameplay.Components;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using jcdcdev.Eco.Core.Services;
using Over9000SignPowerMod.Plugins.Interfaces;

namespace jcdcdev.Eco.Signs.Patterns;

public class StoreIcons : IOver9000SignPowerModPluginTag
{
    private readonly ConcurrentDictionary<Guid, int> _count = new();

    public bool CanProcessed(WorldObject worldObject, User registrar) => true;

    public string ProcessTag(Match match, int tagNum, WorldObject worldObject, User registrar)
    {
        var tick = GetCurrentTick(worldObject.ObjectID);
        var id = Constants.RegexPatterns.Guid.Match(match.Value);
        if (!Guid.TryParse(id.Value, out var guid))
        {
            return Constants.Errors.StoreInvalid;
        }

        var store = StoreService.Data.Get(guid);
        if (store == null)
        {
            return Constants.Errors.StoreNotFound;
        }

        var selling = match.Value.Contains("sell");
        var buying = match.Value.Contains("buy");
        var showPrice = match.Value.Contains("price");
        var showStock = match.Value.Contains("stock");
        var oos = match.Value.Contains("oos");
        if (!buying && !selling)
        {
            buying = true;
            selling = true;
        }

        var items = new List<TradeOffer>();
        if (selling)
        {
            var sellingItems = store.Selling;
            if (!oos)
            {
                sellingItems = sellingItems.Where(x => x.MaxNumWanted > 0).ToList();
            }

            items.AddRange(sellingItems);
        }

        if (buying)
        {
            var buyingItems = store.Buying;
            if (!oos)
            {
                buyingItems = buyingItems.Where(x => x.Stack.Quantity > 0).ToList();
            }

            items.AddRange(buyingItems);
        }

        if (items.Count == 0)
        {
            return string.Empty;
        }

        if (tick > items.Count)
        {
            tick = 1;
        }

        var item = items[tick - 1];
        var output = new StringBuilder();
        if (showPrice || showStock)
        {
            output.Append("<br>");
        }

        if (showStock)
        {
            var stock = item.Buying ? item.MaxNumWanted : item.Stack.Quantity;
            stock = Math.Min(stock, 999);
            var stockText = stock.ToString();
            output.Append($"{stockText}<br>");
        }

        output.Append($"<ecoicon item='{item.Stack.Item?.Name}'>");

        if (showPrice)
        {
            if (showStock)
            {
                output.Append("<br>");
            }

            output.Append(
                $"<br><size=50%><ecoicon name=\"Currency\"> {item.Price.ToString("F").PadRight(5, '0').Substring(0, 5)}</size>");
        }

        tick++;
        SetNextTick(worldObject.ObjectID, tick);
        return output.ToString();
    }

    public string TagName => "store";
    public bool Enabled => true;
    public Regex TagRegex => new("</store (.*?)>", RegexOptions.Compiled);

    private int GetCurrentTick(Guid objectId) => _count.TryGetValue(objectId, out var value) ? value : 1;

    private void SetNextTick(Guid objectId, int tick) => _count[objectId] = tick;
}