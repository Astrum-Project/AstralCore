using System;
using System.Linq;
using System.Reflection;

namespace Astrum.AstralCore.Hooks
{
    public static class Hooks
    {
        public const BindingFlags PrivateStatic = BindingFlags.NonPublic | BindingFlags.Static;

        public static Assembly AssemblyCSharp;

        public static void Initialize(Impl.AnyHarmony harmony)
        {
            Preload();

            // in the future this should be changed to
            // reflectively initalizing everything inside
            // of the Hooks namespace, excluding self
            OnPlayerJL.Initialize(harmony);
        }

        // if this function fails, then Assembly-CSharp has not been loaded
        // i'm not sure why that happened to someone as this mod references it
        // a simple fix would be to add something alphabetically before this mod
        // such as ActionMenuApi or AdvancedSafety
        private static void Preload()
        {
            AssemblyCSharp = AppDomain.CurrentDomain.GetAssemblies().First(f => f.GetName().Name == "Assembly-CSharp");
        }
    }
}
