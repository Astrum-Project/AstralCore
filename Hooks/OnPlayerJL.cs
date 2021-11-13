using Astrum.AstralCore.Types;
using MelonLoader;
using UnityEngine;

namespace Astrum.AstralCore.Hooks
{
    public static class OnPlayerJL
    {
        public static void Initialize(HarmonyLib.Harmony harmony)
        {
            harmony.Patch(Player.Type.GetMethod("OnNetworkReady"), null, typeof(OnPlayerJL).GetMethod(nameof(OnPlayerJL.OnPlayerJoined), Hooks.PrivateStatic).ToNewHarmonyMethod());
            harmony.Patch(Player.Type.GetMethod("OnDestroy"), typeof(OnPlayerJL).GetMethod(nameof(OnPlayerJL.OnPlayerLeft), Hooks.PrivateStatic).ToNewHarmonyMethod());
        }

        private static void OnPlayerJoined(MonoBehaviour __instance) => Events.OnPlayerJoined(new Player(__instance));
        private static void OnPlayerLeft(MonoBehaviour __instance) => Events.OnPlayerLeft(new Player(__instance));
    }
}
