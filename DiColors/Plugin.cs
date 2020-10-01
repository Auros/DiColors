using IPA;
using HarmonyLib;
using System.Reflection;
using IPALogger = IPA.Logging.Logger;

namespace DiColors
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        internal static IPALogger Log { get; private set; }
		private readonly Harmony _harmony;

        [Init]
        public Plugin(IPALogger logger)
        {
            Log = logger;
			_harmony = new Harmony("dev.auros.dicolors");
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