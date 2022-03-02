using DiColors.Installers;
using IPA;
using IPA.Config.Stores;
using IPA.Loader;
using SiraUtil.Zenject;
using Conf = IPA.Config.Config;
using IPALogger = IPA.Logging.Logger;

namespace DiColors;

[NoEnableDisable, Plugin(RuntimeOptions.DynamicInit)]
public class Plugin
{
    [Init]
    public Plugin(Conf conf, IPALogger logger, Zenjector zenjector, PluginMetadata pluginMetadata)
    {
        Config config = conf.Generated<Config>();
        config.Version = pluginMetadata.HVersion;

        zenjector.UseAutoBinder();
        zenjector.UseLogger(logger);
        zenjector.Install<DiColorsUIInstaller>(Location.Menu);
        zenjector.Install<DiColorsCoreInstaller>(Location.App);
        zenjector.Install<DiColorsMenuColorInstaller>(Location.Menu);
        zenjector.Install(Location.App, Container => Container.BindInstance(config));
    }
}