using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace jcdcdev.Eco.Signs;

public class SignConfig
{
    [Description("The rate in seconds (5-120) at which store data is updated. Default: 10s")]
    [Category("Performance")]
    [Range(5, 120)]
    public uint StoreUpdateFrequency { get; set; } = 10;
}