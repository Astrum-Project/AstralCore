using System;
using System.Linq;
using System.Reflection;

namespace Astrum.AstralCore.Hooks
{
    public static class Hooks
    {
        public const BindingFlags PrivateStatic = BindingFlags.NonPublic | BindingFlags.Static;

        public static Assembly AssemblyCSharp;

        public static void Initialize(HarmonyLib.Harmony harmony)
        {
            Preload();

            // in the future this should be changed to
            // reflectively initalizing everything inside
            // of the Hooks namespace, excluding self
            OnRPC.Initialize(harmony);
            OnPlayerJL.Initialize(harmony);
        }

        private static void Preload()
        {
            AssemblyCSharp = AppDomain.CurrentDomain.GetAssemblies().First(f => f.GetName().Name == "Assembly-CSharp");
        }
    }
}
