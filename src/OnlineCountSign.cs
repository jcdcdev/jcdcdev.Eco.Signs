using System.Text.RegularExpressions;
using Over9000SignPowerMod.Plugins.Interfaces;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;

namespace jcdcdev.Eco.Signs;

public class OnlineCountSign : IOver9000SignPowerModPluginTag
{
    public bool CanProcessed(WorldObject worldObject, User registrar)
    {
        return true;
    }

    public string ProcessTag(Match match, int tagNum, WorldObject worldObject, User registrar) =>
        UserManager.OnlineUsers.Count.ToString();

    public string TagName => "onlineCount";
    public bool Enabled => true;

    public Regex TagRegex => new Regex("</onlineCount>", RegexOptions.Compiled);
}