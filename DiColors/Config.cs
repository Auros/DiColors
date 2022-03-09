using IPA.Config.Stores;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using SiraUtil.Converters;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace DiColors;

internal class Config
{
    [NonNullable, UseConverter(typeof(VersionConverter))]
    public virtual Hive.Versioning.Version Version { get; set; } = new Hive.Versioning.Version("0.0.0");

    [UseConverter(typeof(HexColorConverter))]
    public virtual Color MenuFeetColor { get; set; }

    [UseConverter(typeof(HexColorConverter))]
    public virtual Color LogoBeatColor { get; set; }

    [UseConverter(typeof(HexColorConverter))]
    public virtual Color LogoSaberColor { get; set; }

    [UseConverter(typeof(HexColorConverter))]
    public virtual Color DefaultMenuColor { get; set; }

    [UseConverter(typeof(HexColorConverter))]
    public virtual Color ClearedLevelColor { get; set; }

    [UseConverter(typeof(HexColorConverter))]
    public virtual Color FailedLevelColor { get; set; }

    [UseConverter(typeof(HexColorConverter))]
    public virtual Color CountdownColor { get; set; }

    [UseConverter(typeof(HexColorConverter))]
    public virtual Color LobbyColor { get; set; }
}