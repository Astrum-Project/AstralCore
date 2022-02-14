using Astrum.AstralCore.Types;
using UnityEngine;

namespace Astrum.AstralCore.Hooks
{
    public static class OnPlayerJL
    {
        public static void Initialize(Impl.AnyHarmony harmony)
        {
            harmony.Patch(Player.Type.GetMethod("OnNetworkReady"), null, typeof(OnPlayerJL).GetMethod(nameof(OnPlayerJL.OnPlayerJoined), Hooks.PrivateStatic));
            harmony.Patch(Player.Type.GetMethod("OnDestroy"), typeof(OnPlayerJL).GetMethod(nameof(OnPlayerJL.OnPlayerLeft), Hooks.PrivateStatic));
        }

        private static void OnPlayerJoined(MonoBehaviour __instance) => Events.OnPlayerJoined?.Invoke(new Player(__instance));
        private static void OnPlayerLeft(MonoBehaviour __instance) => Events.OnPlayerLeft?.Invoke(new Player(__instance));
    }
}
