using Eco.Core;
using Eco.Core.Plugins;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Shared.Localization;
using Eco.Shared.Utils;

// ReSharper disable once CheckNamespace
namespace jcdcdev.Eco.Core;

public abstract class PluginBase<TConfig> :
    IModKitPlugin,
    IInitializablePlugin,
    IConfigurablePlugin,
    IDisplayablePlugin,
    IThreadedPlugin where TConfig : new()
{
    private static string ModVersion => ModKitExtensions.Version;
    private static string ModName => ModKitExtensions.Name;

    protected bool Active;

    public string GetStatus()
    {
        var sb = new LocStringBuilder();
        sb.AppendLineNTStr($"Version - {ModVersion}");
        sb.AppendLine();
        BuildStatusText(sb);

        return sb.ToString() ?? string.Empty;
    }

    protected virtual void BuildStatusText(LocStringBuilder sb)
    {
    }

    public string GetCategory() => Localizer.DoStr("Mods");

    public IPluginConfig PluginConfig => ConfigBase<TConfig>.PluginConfig;
    public static TConfig Config => ConfigBase<TConfig>.Config;

    public object? GetEditObject() => ConfigBase<TConfig>.PluginConfig.Config;

    public ThreadSafeAction<object, string>? ParamChanged { get; set; }

    public void OnEditObjectChanged(object o, string param) => ConfigBase<TConfig>.OnConfigEntryChanged(o, param);

    public void Initialize(TimedTask timer)
    {
        Log.WriteLine(new LocString($"Initializing {ModName} - {ModVersion}"));
        
        Active = true;
        ConfigBase<TConfig>.Initialize();
        PluginManager.Controller.RunIfOrWhenInited((Action)(() => { }));
        
        InitializeMod(timer);
    }

    public async Task ShutdownAsync()
    {
        Active = false;
        await ShutdownMod();
    }

    protected virtual Task ShutdownMod()
    {
        return Task.CompletedTask;
    }

    public override string ToString()
    {
        return ModName;
    }

    public string GetDisplayText() => GetStatus();

    public void Run()
    {
        RunMod();
    }

    protected virtual void RunMod()
    {
    }

    protected virtual void InitializeMod(TimedTask timer)
    {
    }
}