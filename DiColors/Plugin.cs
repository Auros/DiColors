using IPA;
using HarmonyLib;
using SiraUtil.Zenject;
using System.Reflection;
using DiColors.Installers;
using IPALogger = IPA.Logging.Logger;

namespace DiColors
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        internal static IPALogger Log { get; private set; }
		private readonly Harmony _harmony;

        [Init]
        public Plugin(IPALogger logger, Zenjector zenjector)
        {
            Log = logger;
			_harmony = new Harmony("dev.auros.dicolors");

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