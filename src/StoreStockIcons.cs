using System.Text;
using System.Text.RegularExpressions;
using Eco.Gameplay.Components;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Shared.IoC;
using Over9000SignPowerMod.Controllers;
using Over9000SignPowerMod.Plugins.Interfaces;

namespace jcdcdev.Eco.Signs;

public class StoreStockIcons : IOver9000SignPowerModPluginTag
{
    public bool CanProcessed(WorldObject worldObject, User registrar) => true;

    private int _count = 1;

    public string ProcessTag(Match match, int tagNum, WorldObject worldObject, User registrar)
    {
        var id = Constants.RegexPatterns.Guid.Match(match.Value);
        if (!Guid.TryParse(id.Value, out var guid))
        {
            return string.Empty;
        }

        var store = ServiceHolder<IWorldObjectManager>.Obj.GetFromID(guid);

        var comp = store.GetComponent<StoreComponent>();
        var sb = new StringBuilder("</mulIco \"");
        sb.Append(string.Join(",", comp.AllOffers.Select(x => x.Stack.Item?.Name)));
        sb.Append("\">");
        var ticks = MainController.SimulateTicks(sb.ToString());

        if (_count > comp.AllOffers.Count())
        {
            _count = 1;
        }

        var output = ticks[_count - 1];
        _count++;
        return output;
    }

    public string TagName => "store";
    public bool Enabled => true;
    public Regex TagRegex => new Regex("</store id=\"(.*?)\">", RegexOptions.Compiled);
}