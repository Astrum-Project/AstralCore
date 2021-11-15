using MelonLoader;
using System;

[assembly: MelonInfo(typeof(Astrum.AstralCore.Loader), "AstralCore", "0.3.2", downloadLink: "github.com/Astrum-Project/AstralCore")]
[assembly: MelonGame("VRChat", "VRChat")]
[assembly: MelonColor(ConsoleColor.DarkMagenta)]

namespace Astrum.AstralCore
{
    public class Loader : MelonMod 
    {
        public override void OnApplicationStart()
        {
            Hooks.Hooks.Initialize(HarmonyInstance);

            Events.OnUIInit += Managers.LogManager.Initialize;
        }

        public override void OnUpdate() => Events.OnUpdate();

        public override void OnSceneWasLoaded(int index, string _)
        {
            if (index == 1) Events.OnUIInit();
        }
    }
}
