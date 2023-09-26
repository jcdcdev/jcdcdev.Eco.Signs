namespace jcdcdev.Eco.Signs;

internal class StoreCache
{
    public float? AvgCostPerThousandCalories { get; set; }
    public DateTime Updated { get; set; }
    public Dictionary<Guid, Store> Stores { get; set; } = new();
    public Store? Get(Guid id) => Stores.TryGetValue(id, out var store) ? store : null;
}