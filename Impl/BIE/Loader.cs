extern alias bie;

using BepInEx;
using BepInEx.IL2CPP;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Astrum.AstralCore.Impl.BIE
{
    [BepInPlugin(ImplInfo.GUID, "AstralCore", ImplInfo.Version)]
    public class Loader : BasePlugin
    {
        public override void Load()
        {
            ImplInfo.ImplType = ImplInfo.Implementation.BepInEx;

            LoadBIE();

            SceneManager.add_sceneLoaded((UnityAction<Scene, LoadSceneMode>)SceneLoad);

            Events.OnAppStart += () => Hooks.Hooks.Initialize(new BIEHarmony(new bie::HarmonyLib.Harmony(ImplInfo.GUID)));
            Events.OnUIInit += Managers.LogManager.Initialize;
        }

        public static void SceneLoad(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == -1) Events.OnWorldLoad?.Invoke(scene);
            else if (scene.buildIndex == 0) Events.OnAppStart?.Invoke();
            else if (scene.buildIndex == 1) Events.OnUIInit?.Invoke();
        }

        private void LoadBIE()
        {
            Logger.OnTrace += s => Log.LogDebug(s);
            Logger.OnDebug += s => Log.LogDebug(s);
            Logger.OnInfo += s => Log.LogInfo(s);
            Logger.OnNotif += s => Log.LogMessage(s);
            Logger.OnWarn += s => Log.LogWarning(s);
            Logger.OnError += s => Log.LogError(s);
            Logger.OnFatal += s => Log.LogFatal(s);
        }
    }
}
