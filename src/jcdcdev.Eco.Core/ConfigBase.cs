using Eco.Core.Plugins;

// ReSharper disable once CheckNamespace
namespace jcdcdev.Eco.Core;

public abstract class ConfigBase<T> where T : new()
{
    public static T Config => _config!.Config;
    public static PluginConfig<T> PluginConfig => _config!;
    private static string FileName => ModKitExtensions.Name;
    
    private static PluginConfig<T>? _config;

    public static void Initialize()
    {
        _config = new PluginConfig<T>(ModKitExtensions.Name);
        _config.SaveAsAsync(FileName).GetAwaiter().GetResult();
    }

    public static void OnConfigEntryChanged(object o, string name)
    {
    }
}