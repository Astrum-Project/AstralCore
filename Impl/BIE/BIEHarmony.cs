extern alias bie;

using System.Reflection;

namespace Astrum.AstralCore.Impl.BIE
{
    public class BIEHarmony : AnyHarmony
    {
        private readonly bie::HarmonyLib.Harmony harmony;
        public BIEHarmony(bie::HarmonyLib.Harmony harmony) => this.harmony = harmony;

        public override void Patch(MethodBase original, MethodInfo prefix = null, MethodInfo postfix = null, MethodInfo transpiler = null, MethodInfo finalizer = null, MethodInfo ilmanipulator = null) =>
            harmony.Patch(original, ToHM(prefix), ToHM(postfix), ToHM(transpiler), ToHM(finalizer), ToHM(ilmanipulator));

        private bie::HarmonyLib.HarmonyMethod ToHM(MethodInfo minfo) => minfo == null ? null : new bie::HarmonyLib.HarmonyMethod(minfo);
    }
}
