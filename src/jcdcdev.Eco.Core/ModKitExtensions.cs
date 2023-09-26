using System.Reflection;

// ReSharper disable once CheckNamespace
namespace jcdcdev.Eco.Core;

public static class ModKitExtensions
{
    private static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
    private static AssemblyName AssemblyName => Assembly.GetName();

    public static string Version
    {
        get
        {
            var version = AssemblyName.Version ?? new Version(0, 1, 0);
            return $"{version.Major}.{version.Minor}.{version.Build}";
        }
    }
    
    public static string Name => AssemblyName.Name ?? throw new Exception("Unable to determine mod name");
}