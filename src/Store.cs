using Eco.Gameplay.Components;

namespace jcdcdev.Eco.Signs;

internal class Store
{
    public Store(Guid id, List<TradeOffer> selling, List<TradeOffer> buying)
    {
        Id = id;
        Selling = selling;
        Buying = buying;
    }

    public Guid Id { get; }
    public List<TradeOffer> Selling { get; }
    public List<TradeOffer> Buying { get; }
}