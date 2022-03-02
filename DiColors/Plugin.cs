using DiColors.Installers;
using IPA;
using SiraUtil.Zenject;
using IPALogger = IPA.Logging.Logger;

namespace DiColors;

[NoEnableDisable, Plugin(RuntimeOptions.DynamicInit)]
public class Plugin
{
    [Init]
    public Plugin(IPALogger logger, Zenjector zenjector)
    {
        zenjector.UseAutoBinder();
        zenjector.UseLogger(logger);
        zenjector.Install<DiColorsUIInstaller>(Location.Menu);
        zenjector.Install<DiColorsCoreInstaller>(Location.App);
        zenjector.Install<DiColorsMenuColorInstaller>(Location.Menu);
    }
}