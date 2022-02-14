#pragma warning disable CS0618 // Type or member is obsolete

extern alias ml;

using MelonLoader;
using System;
using System.Linq;
using System.Reflection;

[assembly: MelonInfo(typeof(Astrum.AstralCore.Impl.ML.Loader), nameof(Astrum.AstralCore), Astrum.AstralCore.ImplInfo.Version, downloadLink: "github.com/Astrum-Project/" + nameof(Astrum.AstralCore))]
[assembly: MelonGame("VRChat", "VRChat")]
[assembly: MelonColor(ConsoleColor.DarkMagenta)]
[assembly: MelonOptionalDependencies("BepInEx.Core", "BepInEx.IL2CPP")]

namespace Astrum.AstralCore.Impl.ML
{
    public class Loader : MelonMod 
    {
        static Loader() =>
            new ml::HarmonyLib.Harmony(ImplInfo.GUID).Patch(
                typeof(Assembly).GetMethod(nameof(Assembly.GetTypes)),
                finalizer: typeof(Loader).GetMethod(nameof(FinalizeAssemblyGetTypes), Hooks.Hooks.PrivateStatic).ToNewHarmonyMethod()
            );

        public override void OnApplicationStart()
        {
            ImplInfo.ImplType = ImplInfo.Implementation.MelonLoader;

            SetupLogging();

            Hooks.Hooks.Initialize(new MLHarmony(HarmonyInstance));

            Events.OnUIInit += Managers.LogManager.Initialize;
        }

        public override void OnUpdate() => Events.OnUpdate?.Invoke();

        public override void OnSceneWasLoaded(int index, string _)
        {
            if (index == -1) Events.OnWorldLoad?.Invoke(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
            else if (index == 0) Events.OnAppStart?.Invoke();
            else if (index == 1) Events.OnUIInit?.Invoke();
        }

        // originally from BepinEx/BepInEx.Utility
        private static void FinalizeAssemblyGetTypes(ref Exception __exception, ref Type[] __result)
        {
            if (__exception is not ReflectionTypeLoadException rtle)
                return;

            __exception = null;
            __result = rtle.Types.Where(t => t != null).ToArray();
        }

        private static void SetupLogging()
        {
            Logger.OnInfo += s => MelonLogger.Msg("\r[\x1b[35mAstral \x1b[36mInfo \x1b[0m] \x1b[K" + s);
            Logger.OnNotif += s => MelonLogger.Msg("\r[\x1b[35mAstral \x1b[36mNotif\x1b[0m] \x1b[K" + s);
            Logger.OnWarn += s => MelonLogger.Msg("\r[\x1b[35mAstral \x1b[33mWarn \x1b[0m] \x1b[K" + s);
            Logger.OnError += s => MelonLogger.Msg("\r[\x1b[35mAstral \x1b[31mError\x1b[0m] \x1b[K" + s);
            Logger.OnFatal += s => MelonLogger.Msg("\r[\x1b[35mAstral \x1b[31mFatal\x1b[0m] \x1b[K" + s);

            if (MelonDebug.IsEnabled() || Environment.CommandLine.ToLower().Contains("--astral.debug"))
            {
                Logger.OnTrace += s => MelonLogger.Msg("\r[\x1b[35mAstral \x1b[0mTrace] \x1b[K" + s);
                Logger.OnDebug += s => MelonLogger.Msg("\r[\x1b[35mAstral \x1b[34mDebug\x1b[0m] \x1b[K" + s);

                Logger.Trace("Logger loaded");
            }
        }
    }
}
