using System.Text.RegularExpressions;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Over9000SignPowerMod.Plugins.Interfaces;

namespace jcdcdev.Eco.Signs.Patterns;

public class OnlineCountSign : IOver9000SignPowerModPluginTag
{
    public bool CanProcessed(WorldObject worldObject, User registrar) => true;

    public string ProcessTag(Match match, int tagNum, WorldObject worldObject, User registrar) =>
        UserManager.Users.Count(x => x.IsOnline).ToString();

    public string TagName => "onlineCount";
    public bool Enabled => true;
    public Regex TagRegex => new("</onlineCount>", RegexOptions.Compiled);
}