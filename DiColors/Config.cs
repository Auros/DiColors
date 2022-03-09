using IPA.Config.Stores;
using IPA.Config.Stores.Attributes;
using SiraUtil.Converters;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace DiColors;

internal class Config
{
    [NonNullable, UseConverter(typeof(VersionConverter))]
    public virtual Hive.Versioning.Version Version { get; set; } = new Hive.Versioning.Version("0.0.0");

    public virtual Color MenuFeetColor { get; set; }
    public virtual Color LogoBeatColor { get; set; }
    public virtual Color LogoSaberColor { get; set; }
    public virtual Color DefaultMenuColor { get; set; }
}