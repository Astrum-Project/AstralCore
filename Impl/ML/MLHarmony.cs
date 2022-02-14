extern alias ml;

using MelonLoader;

using System.Reflection;

namespace Astrum.AstralCore.Impl.ML
{
    public class MLHarmony : AnyHarmony
    {
        private readonly ml::HarmonyLib.Harmony harmony;
        public MLHarmony(ml::HarmonyLib.Harmony harmony) => this.harmony = harmony;

        public override void Patch(MethodBase original, MethodInfo prefix = null, MethodInfo postfix = null, MethodInfo transpiler = null, MethodInfo finalizer = null, MethodInfo ilmanipulator = null) =>
            harmony.Patch(original, prefix?.ToNewHarmonyMethod(), postfix?.ToNewHarmonyMethod(), transpiler?.ToNewHarmonyMethod(), finalizer?.ToNewHarmonyMethod(), ilmanipulator?.ToNewHarmonyMethod());
    }
}
