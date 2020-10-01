using IPA;
using HarmonyLib;
using SiraUtil.Zenject;
using System.Reflection;
using DiColors.Installers;
using Conf = IPA.Config.Config;
using IPALogger = IPA.Logging.Logger;
using IPA.Config.Stores;

namespace DiColors
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        internal static IPALogger Log { get; private set; }
		private readonly Harmony _harmony;

        [Init]
        public Plugin(Conf conf, IPALogger logger, Zenjector zenjector)
        {
            Log = logger;
			Config config = conf.Generated<Config>();
			_harmony = new Harmony("dev.auros.dicolors");

			zenjector.OnApp<DiCInstaller>().WithParameters(config);
			zenjector.OnMenu<DiCMenuInstaller>();
			zenjector.OnGame<DiCGameInstaller>();
        }

        [OnEnable]
        public void OnEnable()
        {
			_harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        [OnDisable]
        public void OnDisable()
        {
			_harmony.UnpatchAll();
        }
    }
}